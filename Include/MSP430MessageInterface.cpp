/**
 * MSP430MessageInterface.h
 *
 */

#include "MSP430MessageInterface.h"

bool MSP430MsgAPI::recvPacket(cmdType &cmd, int* dataArray) {
	char tempBuffer[7];

	volatile char incByte;
	incByte = serial.readChar();

	//block until start byte if found
	while(incByte != 0xff) {
		incByte = serial.readChar(); 
	}

	//forces an int to be one of enum variables. if out of range, it'll be an int value that's
	//not within range of enum
	cmd = static_cast<cmdType>(serial.readChar());

	for(int i = 0; i < 7 && incByte!=0xff; i++) {
		incByte = serial.readChar();
		tempBuffer[i] = incByte;
		if (i == 4) packetComplete = true;
	}
	

	dataArray[0] = conv8bits_to_16bit(tempBuffer, 0);
	dataArray[1] = conv8bits_to_16bit(tempBuffer, 2);
	dataArray[2] = conv8bits_to_16bit(tempBuffer, 4);

	int chkSum = dataArray[0] + dataArray[1] + dataArray[2];

	//Check if valid
	return (chkSum & 0xff) == tempBuffer[6];
}

void MSP430MsgAPI::sendPacket(cmdType cmd, const unsigned int& data0, const unsigned int& data1, const unsigned int& data2) {

	char txBuffer[PACKET_SIZE];
	txBuffer[0] = 0xff;
	txBuffer[1] = cmd;
	conv16bit_to_8bits(data0, txBuffer, 2);
	conv16bit_to_8bits(data1, txBuffer, 4);
	conv16bit_to_8bits(data2, txBuffer, 6);
	txBuffer[8] = (data0+data1+data2) & 0xff;

	for(int i = 0; i < PACKET_SIZE; i++) {
		serial.printChar(txBuffer[i]);
	}
}
