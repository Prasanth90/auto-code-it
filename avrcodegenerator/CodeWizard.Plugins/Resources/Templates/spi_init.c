

struct spi_device $SpiName$_SPI_DEVICE = {
	/* ! Board specific select id */
	.id = $SpiName$_SPI_CS
};

void $SpiName$_init(void)
{
	/* Initialize SPI in master mode to access the transceiver */
	spi_master_init($SpiName$_SPI);
	spi_master_setup_device($SpiName$_SPI, &$SpiName$_SPI_DEVICE, $SpiMode$,
			$BaudRate$, 0);
	spi_enable($SpiName$_SPI);
}