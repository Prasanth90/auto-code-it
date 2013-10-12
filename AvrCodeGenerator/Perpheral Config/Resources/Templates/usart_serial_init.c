// USART options.

void $UsartName$_init()
{
static usart_rs232_options_t USART_SERIAL_OPTIONS = {
	.baudrate = $UsartName$_SERIAL_BAUDRATE,
	.charlength = $UsartName$_SERIAL_CHAR_LENGTH,
	.paritytype = $UsartName$_SERIAL_PARITY,
	.stopbits = $UsartName$_SERIAL_STOP_BIT
};

// Initialize usart driver in RS232 mode
usart_init_rs232($UsartName$_SERIAL_NAME, &USART_SERIAL_OPTIONS);

$InteruptInit$
}