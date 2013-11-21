void $TimerName$_channel_$SelectedTimerChannel$_init()
{
	tc_enable_cc_channels(&$TimerName$,(enum tc_cc_channel_mask_enable_t)($SelectedTimerChannelEnable$));
	tc_write_cc(&$TimerName$,$SelectedTimerChannel$,$ChannelPeriod$);
	$TimerChannelInterrupt$
}