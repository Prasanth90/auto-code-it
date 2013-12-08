void $TimerName$_init()
{
	tc_enable(&$TimerName$);
	tc_write_clock_source(&$TimerName$,$SelectedClockSource$);
	tc_set_wgm(&$TimerName$, $SelectedWavwformMode$);
	tc_write_count(&$TimerName$,$TimerCount$);
	tc_write_period(&$TimerName$, $TimerPeriod$);
	$TimerOverflowInteruptInit$
	$TimerModeInit$
	$TimerChannelsInitFunCall$
}

$TimerChannelsInitFuncDefine$

