#ifndef _CONF_USART_OPTIONS_H
#define _CONF_USART_OPTIONS_H

#include "conf_board.h"
#include "sysclk.h"

/*! \name Configuration
 */
//! @{
#define $UsartName$_SERIAL_NAME           &$UsartName$
#define $UsartName$_SERIAL_BAUDRATE		  $BaudRate$
#define $UsartName$_SERIAL_CHAR_LENGTH    $UsartCharSize$
#define $UsartName$_SERIAL_PARITY         $UsartParityMode$
#define $UsartName$_SERIAL_STOP_BIT       $UsartStopBit$
//! @}

#endif // _CONF_USART_EXAMPLE_H