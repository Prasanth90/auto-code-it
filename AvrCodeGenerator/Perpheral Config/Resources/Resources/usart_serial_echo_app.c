	uint8_t tx_buf[] = "\n\rHello AVR world ! : ";
	uint8_t tx_length = 22;
	uint8_t received_byte;
	uint8_t i;

	/* Initialize the board.
	 * The board-specific conf_board.h file contains the configuration of
	 * the board initialization.
	 */
	board_init();
	sysclk_init();

	$UsartSerialInit$

	// Send "message header"
	for (i = 0; i < tx_length; i++) {
		usart_putchar($UsartName$_SERIAL_NAME, tx_buf[i]);
	}
	// Get and echo a character forever, specific '\r' processing.
	while (true)
	 {
		received_byte = usart_getchar(USART_SERIAL_NAME);
		if (received_byte == '\r')
		 {
			for (i = 0; i < tx_length; i++)
			 {
				usart_putchar($UsartName$_SERIAL_NAME, tx_buf[i]);
			 }
		} 
		else
		{
			usart_putchar($UsartName$_SERIAL_NAME, received_byte);
		}
	}