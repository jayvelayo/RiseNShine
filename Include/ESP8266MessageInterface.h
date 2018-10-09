/**
 * ESP8266MessageInterface.h
 *
 */

#ifndef ESP8266MessageInterface_H_
#define ESP8266MessageInterface_H_

#include "messageAPI.h"
#include <HardwareSerial.h>
#include <Stream.h>

extern HardwareSerial Serial;


class ESP8266MsgAPI : public MessageAPI
{
public:
	/**
	 * sendPacket - sends the packet thru UART
	 * @param:	cmd the command type as listed in the _cmd_list
	 * @param:	data0 the first data of type int to send
	 * @param:	data1 the second data to be sent
	 * @param:	data2 the third data to be sent
	 */
	void sendPacket(cmdType cmd, const unsigned int& data0, const unsigned int& data1, const unsigned int& data2);
	
	/**
	 * recvPacket - reads the UART RX buffer to parse cmd and data
	 * @param:	cmd - pointer of the resulting cmd char
	 * @param:	dataArray - pointer of array of size 3 for data to be placed
	 * @post:	changes the value in cmd and data array depending on the received packet
	 * @return:	bool states whether the received data is valid
	 */
	bool recvPacket(cmdType& cmd, int* dataArray);
};

#endif /* ESP8266MessageInterface_H_ */
