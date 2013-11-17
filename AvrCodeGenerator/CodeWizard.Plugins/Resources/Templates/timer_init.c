void $TimerName$_init()
{
	tc_enable(&$TimerName$);
	tc_set_wgm(&$TimerName$, $SelectedWavwformMode$);
	tc_write_period(&$TimerName$, $TimerPeriod$);
	$timerChannelsInit$
}

