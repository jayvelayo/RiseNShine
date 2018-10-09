/**
 * MSP430MessageInterface.h
 *
 */

#ifndef MSP430MessageInterface_H_
#define MSP430MessageInterface_H_

#include "messageAPI.h"
#include "SerialLibrary.hpp"

class MSP430MsgAPI : public MessageAPI
{
private:
	SerialPort& serial;
public:

	/* Constructor
	 * @param: 	serial An existing instance of SerialPort. Since the functionality
	 * 			of this class is dependent on the existence of SerialPort,
	 *			the lifeline of the two object is linked.
	 */
	MSP430MsgAPI(SerialPort& serial) : serial(serial) {}

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
	bool recvPacket(cmdType &cmd, int* dataArray);
};

#endif /* MSP430MessageInterface_H_ */
