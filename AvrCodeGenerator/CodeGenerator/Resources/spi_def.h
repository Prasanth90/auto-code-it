#define $SpiName$_SPI                  &$SpiName$
#define $SpiName$_SPI_CS               IOPORT_CREATE_PIN($SpiCsPort$, $SpiCsPin$)
#define $SpiName$_SPI_MOSI             IOPORT_CREATE_PIN($SPIPort$, 5)
#define $SpiName$_SPI_MISO             IOPORT_CREATE_PIN($SPIPort$, 6)
#define $SpiName$_SPI_SCK              IOPORT_CREATE_PIN($SPIPort$, 7)