VFX = {}
VFX[0x0200] = "Bmr_Hit" #512
VFX[0x0201] = "Bmr_etr02_2" #513
VFX[0x0202] = "Bmr_etr01" #514
VFX[0x0203] = "Bmr_etr02" #515
VFX[0x0204] = "Bmr_etr03" #516
VFX[0x0206] = "Bmr_LineHL" #518
VFX[0x0207] = "Bmr_EBrs" #519
VFX[0x0208] = "Bmr_EBrs2" #520
VFX[0x0209] = "Bmr_LineHR" #521
VFX[0x0136] = "BSpk_etr" #310
VFX[0x014A] = "Kkr_Bas" #330
VFX[0x0154] = "KIzn_etr" #340
VFX[0x0262] = "Rst_Impt" #610
VFX[0x026C] = "SAtk_Bas" #620
VFX[0x01F4] = "Bgk_etr" #500
VFX[0x01F5] = "Bgk_Iznup" #501
VFX[0x01F6] = "Bgk_IznDm" #502
VFX[0x01F7] = "Bgk_Line01" #503
VFX[0x01F8] = "Bgk_Line02" #504
VFX[0x01F9] = "Bgk_LineDMG" #505
VFX[0x01FA] = "Bgk_Jmp" #506
VFX[0x01FE] = "Bmr_WBas" #510
VFX[0x01FF] = "Bmr_WFog" #511
VFX2 = {}
VFX2[0x0200] = "Bmr_SHit2" #512
VFX2[0x012C] = "Hzt_Hit" #300
VFX2[0x01FE] = "Bmr_SHit" #510
VFX2[0x01FF] = "Bmr_SHitH" #511
OBJ = []
OBJ.append("GUY_BAS_STAND")# 00
OBJ.append("GUY_BAS_CROUCH")# 01
OBJ.append("GUY_BAS_TURN_STAND")# 02
OBJ.append("GUY_BAS_TURN_CROUCH")# 03
OBJ.append("GUY_BAS_STAND_CROUCH")# 04
OBJ.append("GUY_BAS_CROUCH_STAND")# 05
OBJ.append("GUY_BAS_FORWARD")# 06
OBJ.append("GUY_BAS_BACKWARD")# 07
OBJ.append("GUY_BAS_STAND_JUMP")# 08
OBJ.append("GUY_BAS_JUMP")# 09
OBJ.append("GUY_BAS_JUMP_LAND")# 10
OBJ.append("GUY_BAS_STAND_JUMP_FRONT")# 11
OBJ.append("GUY_BAS_JUMP_FRONT")# 12
OBJ.append("GUY_BAS_JUMP_LAND_FRONT")# 13
OBJ.append("GUY_BAS_STAND_JUMP_BACK")# 14
OBJ.append("GUY_BAS_JUMP_BACK")# 15
OBJ.append("GUY_BAS_JUMP_LAND_BACK")# 16
OBJ.append("GUY_BAS_DASH_FRONT")# 17
OBJ.append("GUY_BAS_DASH_BACK")# 18
OBJ.append("GUY_BAS_DOWN_AOMUKE")# 19
OBJ.append("GUY_BAS_DOWN_UTSUBUSE")# 20
OBJ.append("GUY_BAS_DOWN_STAND_AOMUKE")# 21
OBJ.append("GUY_BAS_DOWN_STAND_UTSUBUSE")# 22
OBJ.append("GUY_BAS_DOWN_STUN_AOMUKE")# 23
OBJ.append("GUY_BAS_DOWN_STUN_UTSUBUSE")# 24
OBJ.append("GUY_BAS_STUN")# 25
OBJ.append("GUY_BAS_NAGE_FAILED")# 26
OBJ.append("GUY_BAS_NAGE_ESCAPE")# 27
OBJ.append("GUY_GRD_START")# 28
OBJ.append("GUY_GRD_START_C")# 29
OBJ.append("GUY_GRD_HL")# 30
OBJ.append("GUY_GRD_HH")# 31
OBJ.append("GUY_GRD_ML")# 32
OBJ.append("GUY_GRD_MH")# 33
OBJ.append("GUY_GRD_LL")# 34
OBJ.append("GUY_GRD_LH")# 35
OBJ.append("GUY_GRD_CL")# 36
OBJ.append("GUY_GRD_CH")# 37
OBJ.append("GUY_GRD_END")# 38
OBJ.append("GUY_GRD_END_C")# 39
OBJ.append("GUY_DMG_HL")# 40
OBJ.append("GUY_DMG_HM")# 41
OBJ.append("GUY_DMG_HH")# 42
OBJ.append("GUY_DMG_ML")# 43
OBJ.append("GUY_DMG_MM")# 44
OBJ.append("GUY_DMG_MH")# 45
OBJ.append("GUY_DMG_LL")# 46
OBJ.append("GUY_DMG_LM")# 47
OBJ.append("GUY_DMG_LH")# 48
OBJ.append("GUY_DMG_UP")# 49
OBJ.append("GUY_DMG_DOWN")# 50
OBJ.append("GUY_DMG_CL")# 51
OBJ.append("GUY_DMG_CM")# 52
OBJ.append("GUY_DMG_CH")# 53
OBJ.append("GUY_DMG_HJ")# 54
OBJ.append("GUY_DMG_HJ_FUKKI")# 55
OBJ.append("GUY_DMG_MJ")# 56
OBJ.append("GUY_DMG_MJ_FUKKI")# 57
OBJ.append("GUY_DMG_TURN_L")# 58
OBJ.append("GUY_DMG_TURN_R")# 59
OBJ.append("GUY_DMG_BLOW")# 60
OBJ.append("GUY_DMG_BLOW_UP")# 61
OBJ.append("GUY_DMG_SPIN")# 62
OBJ.append("GUY_DMG_ASHIBARAI")# 63
OBJ.append("GUY_DMG_BLOW_BOUND")# 64
OBJ.append("GUY_DMG_BOUND_S_AOMUKE")# 65
OBJ.append("GUY_DMG_BOUND_L_AOMUKE")# 66
OBJ.append("GUY_DMG_BOUND_S_UTSUBUSE")# 67
OBJ.append("GUY_DMG_BOUND_L_UTSUBUSE")# 68
OBJ.append("GUY_DMG_WALL_BLOW")# 69
OBJ.append("GUY_DMG_WALL_BOUND_KUZURE")# 70
OBJ.append("GUY_DMG_WALL_BOUND_UTUBUSE")# 71
OBJ.append("DSM_DMG_BURN_CROUCH")# 72
OBJ.append("RYU_DMG_BURN_JUMP")# 73
OBJ.append("GUY_DMG_BOUND_S_AOMUKE")# 74
OBJ.append("GUY_DMG_KUZURE_STAND")# 75
OBJ.append("GUY_DMG_KUZURE_CROUCH")# 76
OBJ.append("GUY_ATK_5LP")# 77
OBJ.append("GUY_ATK_5LP")# 78
OBJ.append("GUY_ATK_5MP")# 79
OBJ.append("GUY_ATK_5MPF")# 80
OBJ.append("GUY_ATK_5HP")# 81
OBJ.append("GUY_ATK_5HPF")# 82
OBJ.append("GUY_ATK_5LK")# 83
OBJ.append("GUY_ATK_5LKF")# 84
OBJ.append("GUY_ATK_5MK")# 85
OBJ.append("GUY_ATK_5MKF")# 86
OBJ.append("GUY_ATK_5HK")# 87
OBJ.append("GUY_ATK_5HKF")# 88
OBJ.append("GUY_ATK_2LP")# 89
OBJ.append("GUY_ATK_2MP")# 90
OBJ.append("GUY_ATK_2HP")# 91
OBJ.append("GUY_ATK_2LK")# 92
OBJ.append("GUY_ATK_2MK")# 93
OBJ.append("GUY_ATK_2HK")# 94
OBJ.append("GUY_ATK_8LP")# 95
OBJ.append("GUY_ATK_8LP")# 96
OBJ.append("GUY_ATK_8MP")# 97
OBJ.append("GUY_ATK_9MP")# 98
OBJ.append("GUY_ATK_8HP")# 99
OBJ.append("GUY_ATK_8HP")# 100
OBJ.append("GUY_ATK_8LK")# 101
OBJ.append("GUY_ATK_9LK")# 102
OBJ.append("GUY_ATK_8MK")# 103
OBJ.append("GUY_ATK_8MK")# 104
OBJ.append("GUY_ATK_8HK")# 105
OBJ.append("GUY_ATK_9HK")# 106
OBJ.append("GUY_ATK_3HK")# 107
OBJ.append("GUY_ATK_79")# 108
OBJ.append("GUY_ATK_SAVING")# 109
OBJ.append("GUY_NGA_SUKARI")# 110
OBJ.append("GUY_NGA_6")# 111
OBJ.append("GUY_NGA_4")# 112
OBJ.append("GUY_NGD_6")# 113
OBJ.append("GUY_NGD_4")# 114
OBJ.append("GUY_SPA_HOUZANTOU")# 115
OBJ.append("GUY_SPA_SENPUUKYAKU")# 116
OBJ.append("GUY_SPA_HAYAGAKE_01")# 117
OBJ.append("GUY_SPA_HAYAGAKE_02")# 118
OBJ.append("GUY_SPA_HAYAGAKE_03")# 119
OBJ.append("GUY_SPA_HAYAGAKE_04")# 120
OBJ.append("GUY_SPA_HAYAGAKE_05")# 121
OBJ.append("GUY_SPA_IZUNAOTOSHI_01")# 122
OBJ.append("GUY_SPA_IZUNAOTOSHI_02")# 123
OBJ.append("GUY_SPA_IZUNAOTOSHI_03")# 124
OBJ.append("GUY_SPA_IZUNAOTOSHI_04")# 125
OBJ.append("GUY_SPD_IZUNAOTOSHI_04")# 126
OBJ.append("GUY_SPA_KAITENIZUNA_01")# 127
OBJ.append("GUY_SPA_KAITENIZUNA_02")# 128
OBJ.append("GUY_SPD_KAITENIZUNA_02")# 129
OBJ.append("GUY_ATK_6MP")# 130
OBJ.append("GUY_DEM_START_01_01")# 131
OBJ.append("GUY_DEM_START_01_02")# 132
OBJ.append("GUY_DEM_START_01_03")# 133
OBJ.append("GUY_DEM_START_01_04")# 134
OBJ.append("GUY_DEM_WIN_01")# 135
OBJ.append("GUY_DEM_WIN_02")# 136
OBJ.append("GUY_DEM_WIN_03")# 137
OBJ.append("GUY_DEM_LOSE_01")# 138
OBJ.append("GUY_DEM_RESULT_01_01")# 139
OBJ.append("GUY_DEM_RESULT_01_02")# 140
OBJ.append("GUY_DEM_RESULT_01_03")# 141
OBJ.append("GUY_DEM_RESULT_01_04")# 142
OBJ.append("GUY_DEM_APPEAL_01")# 143
OBJ.append("GUY_DEM_APPEAL_02")# 144
OBJ.append("GUY_DEM_APPEAL_03")# 145
OBJ.append("GUY_DEM_APPEAL_04")# 146
OBJ.append("GUY_DEM_APPEAL_05")# 147
OBJ.append("GUY_DEM_APPEAL_06")# 148
OBJ.append("GUY_DEM_APPEAL_07")# 149
OBJ.append("GUY_DEM_APPEAL_08")# 150
OBJ.append("GUY_DEM_APPEAL_09")# 151
OBJ.append("GUY_DEM_APPEAL_10")# 152
OBJ.append("GUY_DEM_CONTINUE_01")# 153
OBJ.append("GUY_DEM_CONTINUE_02")# 154
OBJ.append("GUY_DEM_CONTINUE_03")# 155
OBJ.append("GUY_DEM_BOSS_START_STAND")# 156
OBJ.append("GUY_SCA_HASSOUKEN_01")# 157
OBJ.append("GUY_SCA_HASSOUKEN_02")# 158
OBJ.append("GUY_SCA_HASSOUKEN_03")# 159
OBJ.append("GUY_SCA_HASSOUKEN_04")# 160
OBJ.append("GUY_SCA_HASSOUKEN_05")# 161
OBJ.append("GUY_SCA_HASSOUKEN_06")# 162
OBJ.append("GUY_DEM_WIN_04")# 163
FCE = []
FCE.append("GUY_FCE_DEFAULT")# 00
FCE.append("GUY_FCE_SPK_A")# 01
FCE.append("GUY_FCE_SPK_I")# 02
FCE.append("GUY_FCE_SPK_U")# 03
FCE.append("GUY_FCE_SPK_E")# 04
FCE.append("GUY_FCE_SPK_O")# 05
FCE.append("GUY_FCE_SPK_N")# 06
FCE.append("GUY_FCE_SPK_A_ACCENT")# 07
FCE.append("GUY_FCE_SPK_I_ACCENT")# 08
FCE.append("GUY_FCE_SPK_U_ACCENT")# 09
FCE.append("GUY_FCE_SPK_E_ACCENT")# 10
FCE.append("GUY_FCE_SPK_O_ACCENT")# 11
FCE.append("GUY_FCE_SPK_N_ACCENT")# 12
FCE.append("GUY_FCE_BHV_BLINK")# 13
FCE.append("_blank")# 14
FCE.append("_blank1")# 15
FCE.append("_blank2")# 16
FCE.append("_blank3")# 17
FCE.append("GUY_FCE_EMO_ANGER")# 18
FCE.append("GUY_FCE_EMO_SHOUT")# 19
FCE.append("GUY_FCE_EMO_HATE")# 20
FCE.append("GUY_FCE_EMO_JOY")# 21
FCE.append("GUY_FCE_EMO_SMILE")# 22
FCE.append("GUY_FCE_EMO_FEAR")# 23
FCE.append("GUY_FCE_EMO_SORROW")# 24
FCE.append("GUY_FCE_EMO_CRY")# 25
FCE.append("GUY_FCE_EMO_SURPRISE")# 26
FCE.append("GUY_FCE_EMO_TIREDNESS")# 27
FCE.append("GUY_FCE_EMO_ATACK")# 28
FCE.append("GUY_FCE_EMO_ATACK_ACCENT")# 29
FCE.append("GUY_FCE_EMO_AWAKENING")# 30
FCE.append("_blank4")# 31
FCE.append("_blank5")# 32
FCE.append("GUY_FCE_DMG_HEAD_R")# 33
FCE.append("GUY_FCE_DMG_HEAD_L")# 34
FCE.append("GUY_FCE_DMG_HEAD_ACCENT_R")# 35
FCE.append("GUY_FCE_DMG_HEAD_ACCENT_L")# 36
FCE.append("GUY_FCE_DMG_UPPER")# 37
FCE.append("GUY_FCE_DMG_UPPER_ACCENT")# 38
FCE.append("GUY_FCE_DMG_BODY")# 39
FCE.append("GUY_FCE_DMG_VOMIT")# 40
FCE.append("GUY_FCE_DMG_THUNDER")# 41
FCE.append("GUY_FCE_DMG_DOWN")# 42
FCE.append("GUY_FCE_DMG_DOWN_ACCENT")# 43
FCE.append("_blank6")# 44
FCE.append("_blank7")# 45
FCE.append("_blank8")# 46
FCE.append("GUY_FCE_UNQ_ANGER")# 47
FCE.append("_blank9")# 48
CAM = []
CAM.append("GUY_CAM_KAITENIZUNA_02")# 00
CAM.append("GUY_CAM_GOURAI_02")# 01
CAM.append("GUY_CAM_RENKA_02")# 02
CAM.append("GUY_CAM_START_01_01")# 03
CAM.append("GUY_CAM_START_01_02")# 04
CAM.append("GUY_CAM_START_01_03")# 05
CAM.append("GUY_CAM_START_01_04")# 06
CAM.append("GUY_CAM_RESULT_01_01")# 07
CAM.append("GUY_CAM_RESULT_01_02")# 08
CAM.append("GUY_CAM_RESULT_01_03")# 09
CAM.append("GUY_CAM_CONTINUE_01")# 10
CAM.append("GUY_CAM_CONTINUE_02")# 11
CAM.append("GUY_CAM_CONTINUE_03")# 12
CAM.append("GUY_CAM_RESULT_01_04")# 13
CAM.append("GUY_CAM_RIVAL_01_01")# 14
CAM.append("GUY_CAM_RIVAL_01_02")# 15
CAM.append("GUY_CAM_RIVAL_01_03")# 16
CAM.append("GUY_CAM_RIVAL_01_04")# 17
CAM.append("GUY_CAM_RIVAL_01_05")# 18
CAM.append("GUY_CAM_RIVAL_01_06")# 19
CAM.append("GUY_CAM_RIVAL_01_07")# 20
CAM.append("GUY_CAM_RIVAL_01_08")# 21
CAM.append("GUY_CAM_RIVAL_01_09")# 22
CAM.append("GUY_CAM_RIVAL_01_10")# 23
UC1 = []
UC1.append("GUY_UCA_GOURAI_01")# 00
UC1.append("GUY_UCA_GOURAI_02")# 01
UC1.append("GUY_UCD_GOURAI_02")# 02
UC2 = []
UC2.append("GUY_UCA_RENKA_01")# 00
UC2.append("GUY_UCA_RENKA_02")# 01
UC2.append("GUY_UCD_RENKA_02")# 02
