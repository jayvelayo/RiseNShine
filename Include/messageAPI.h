/**
 * messageAPI.h
 *
 */

#ifndef MessageAPI_H_
#define MessageAPI_H_

#define PACKET_SIZE 9

typedef enum _packet_index {
	startbyte,
	cmdbyte,
	databyte0_MSB,
	databyte0_LSB,
	databyte1_MSB,
	databyte1_LSB,
	databyte2_MSB,
	databyte2_LSB,
	chkbyte
} pktIndex;

typedef enum _cmd_list {
	getSunsetTime = 'a',
	getSunriseTime = 'b',
	getCurrentTime = 'c'
} cmdType;

class MessageAPI {
public:
    virtual void sendPacket(cmdType cmd, const unsigned int& data0, const unsigned int& data1, const unsigned int& data2) = 0;
    virtual bool recvPacket(cmdType& cmd, int* dataArray) = 0;

protected:
    void conv16bit_to_8bits(const unsigned int& data, char* array, const unsigned int& i);
	int conv8bits_to_16bit(const char* array, const unsigned int& index);
};

#endif /* #ifndef MessageAPI */
