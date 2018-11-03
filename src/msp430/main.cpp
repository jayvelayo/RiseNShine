#include <msp430.h> 
#include "MSP430MessageInterface.h" //for MSP430MsgAPI class
#include "stepperRiseNShine.h" // for SerialPort and Stepper class
#include "timedata.h" // for timedata class


SerialPort serial;
MSP430MsgAPI api(serial);
Stepper stepper1(1,3,4,5,6);

// flags
bool stepper1flag = false;
bool isPacketReady = false;
bool updateSunsetTimeFlag = false;
bool updateSunriseTimeFlag = false;
bool updateCurrentTimeFlag = false;
bool isSunsetTime = false;
bool isSunriseTime = false;

// time storage
timedata currentTime;
timedata sunsetTime;
timedata sunriseTime;

// scheduler when to update current time
timedata updateCurrentTimeSchedule(1,0,0); //1:00:00 AM
timedata updateSunriseTimeSchedule(1,30,0); //1:30:00 AM
timedata updateSunsetTimeSchedule(2,0,0); //2:00:00 AM

void parsePacket(cmdType &cmd, int (&data)[3]);

/**
 * main.c
 */
int main(void)
{
  WDTCTL = WDTPW + WDTHOLD;                 // Stop WDT

    /*
     * Clock Set up to 1MHz using DCO
     */
    if (CALBC1_12MHZ==0xFF)                    // If calibration constant erased
    {
      while(1);                               // do not load, trap CPU!!
    }
    DCOCTL = 0;                               // Select lowest DCOx and MODx settings
    BCSCTL1 = CALBC1_12MHZ;                    // Set range
    DCOCTL = CALDCO_12MHZ;                     // Set DCO step + modulation */
    BCSCTL1 |= DIVA_3;              // ACLK/8
    BCSCTL3 |= XCAP_3;              //12.5pF cap- setting for 32768Hz crystal

    /*
     * Port setup
     */
    P1DIR = 0xFF;
    P2DIR = 0xFF;

    /*
     * Timer A0 - Timekeeping
     */
    TA0CCTL0 = CCIE;                   // CCR0 interrupt enabled
    TA0CCR0 = 511;                 // 512 -> 1 sec, 30720 -> 1 min
    TA0CTL = TASSEL_1 + ID_3 + MC_1;         // ACLK, /8, upmode

    /*
     * Timer A1 - Stepper motor speed
     */
    TA1CTL = TASSEL_2 + MC_2; //SMCLK, Cont. mode
    TA1CCR1 = 1000; //Stepper 1 Speed
    TA1CCR2 = 40000; //Stepper 2 speed
    TA1CCTL1 = CCIE;
    TA1CCTL2 &= ~CCIE;

    /*
     * UART Setup
     */
    serial.begin(9600);

    /*
     * Data storage for packet results from API
     */
    cmdType cmd;
    int data[3];

    //SETUP ENDS HERE



    while(1) {

        if (updateCurrentTimeFlag) {
            updateCurrentTimeFlag = false;
            api.sendPacket(cmdType::getCurrentTime, 0, 0, 0);
        }
        if (updateSunsetTimeFlag) {
            updateSunsetTimeFlag = false;
            api.sendPacket(cmdType::getSunsetTime, 0, 0, 0);
        }
        if (updateSunriseTimeFlag) {
            updateSunriseTimeFlag = false;
            api.sendPacket(cmdType::getSunriseTime, 0,0,0);
        }

        isPacketReady = api.recvPacket(cmd, data);

        if (isPacketReady) {
            parsePacket(cmd, data);
        }

        if(stepper1flag) {
            stepper1flag = false;
            stepper1.applyState();
        }

        if (isSunrise) {
            isSunrise = false;
            stepper1.setToClockwiseRotation(false);
            stepper1.moveStepper(4096+4096+4096); //4096 step = 1 rotation; move CW three times
        }

        if (isSunset) {
            isSunset = false;
            stepper1.setToClockwiseRotation(true);
            stepper1.moveStepper(4096+4096+4096); //4096 step = 1 rotation; move CW three times
        }

        __bis_SR_register(LPM3_bits + GIE);

    }
}

void parsePacket(cmdType& cmd, int (&data)[3]) {
    switch(cmd) {
    case cmdType::getSunsetTime:
        sunsetTime.updateValues(data);
        break;
    case cmdType::getSunriseTime:
        sunriseTime.updateValues(data);
        break;
    case cmdType::getCurrentTime:
        currentTime.updateValues(data);
        break;
    default:
        break;
    }
}

#pragma vector=TIMER1_A1_VECTOR
__interrupt void Timer_A1(void)
{
  switch( TA1IV )
  {
  case  0: break;
  //CCR1 interrupt
  case  2:
      stepper1flag = true;
      break;
  //CCR2 interrupt
  case 4: break;
  }
}

// Timer A0 interrupt service routine
#pragma vector=TIMER0_A0_VECTOR
__interrupt void Timer_A (void)
{
    currentTime.incrementSecond();

    if( updateCurrentTimeSchedule == currentTime) {
        updateCurrentTimeFlag = true;
    }
    else if (updateSunriseTimeSchedule == currentTime) {
        updateSunriseTimeFlag = true;
    }
    else if (updateSunsetTimeSchedule == currentTime) {
        updateSunsetTimeFlag = true;
    }

    if (currentTime == sunriseTime) isSunrise = true;
    if (currentTime == sunsetTime) isSunset = true;

    P1OUT ^= BIT0;                  // Toggle LED
    __bic_SR_register_on_exit(LPM3_bits);
}
