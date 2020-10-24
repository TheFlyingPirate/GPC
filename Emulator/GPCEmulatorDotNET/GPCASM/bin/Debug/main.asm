.start
	LDA #0x02
	LDB #0x03
	STB .data+2
.main
	JSR .mult
	JMP .main
.mult
	STB .data
	LDB #0xFF
	ADD .data+1
	LDA .data+2
	LDB .data
	ADD .data+2
	LDA .data+1
	LDB #0x00
	JEQ .done
	RSR
.done
	LDA .data
	STA .data+3
.loop
	NOP
	JMP .loop
.data