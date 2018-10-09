/**
 * ESP8266MessageInterface.cpp
 *
 */

#include "ESP8266MessageInterface.h"

bool ESP8266MsgAPI::recvPacket(cmdType& cmd, int* dataArray) {
	char tempBuffer[7];
	bool packetComplete = false;

	volatile char incByte;
	while(!packetComplete) {
		incByte = Serial.read();
		while(incByte != 0xff) {
			incByte = Serial.read(); 
		}

		//forces an int to be one of enum variables. if out of range, it'll be an int value that's
		//not within range of enum
		cmd = static_cast<cmdType>(Serial.read());

		for(int i = 0; i < 7 && incByte!=0xff; i++) {
			incByte = Serial.read();
			tempBuffer[i] = incByte;
			if (i == 4) packetComplete = true;
		}
	}

	dataArray[0] = conv8bits_to_16bit(tempBuffer, 0);
	dataArray[1] = conv8bits_to_16bit(tempBuffer, 2);
	dataArray[2] = conv8bits_to_16bit(tempBuffer, 4);

	int chkSum = dataArray[0] + dataArray[1] + dataArray[2];

	//Check if valid
	return (chkSum & 0xff) == tempBuffer[6];
}

void ESP8266MsgAPI::sendPacket(cmdType cmd, const unsigned int& data0, const unsigned int& data1, const unsigned int& data2) {

	char txBuffer[PACKET_SIZE];
	txBuffer[0] = 0xff;
	txBuffer[1] = cmd;
	conv16bit_to_8bits(data0, txBuffer, 2);
	conv16bit_to_8bits(data0, txBuffer, 4);
	conv16bit_to_8bits(data0, txBuffer, 6);
	txBuffer[8] = (data0+data1+data2) & 0xff;

	for(int i = PACKET_SIZE; i > 0; i--) {
		Serial.print(txBuffer[i]);
	}
}