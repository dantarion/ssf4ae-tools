VFX = {}
VFX[0x0200] = "SOE_Tnd" #512
VFX[0x0201] = "SOE_ImptS" #513
VFX[0x0202] = "SOE_ImptG" #514
VFX[0x0203] = "SOE_End" #515
VFX[0x0204] = "SOE_ImptG2" #516
VFX[0x026C] = "SAtk_Bas" #620
VFX[0x0262] = "Rst_Impt" #610
VFX[0x012C] = "EThd_Bas" #300
VFX[0x012D] = "EThdEX_Bas" #301
VFX[0x012E] = "EThd_End" #302
VFX[0x0190] = "GSR_Bas" #400
VFX[0x0191] = "GSR_Run" #401
VFX[0x0263] = "Rst_tail" #611
VFX[0x01F4] = "LCb_Bas" #500
VFX[0x01F5] = "LCb_Shout" #501
VFX[0x01F6] = "LCb_Run" #502
VFX[0x01F7] = "LCb_Crg" #503
VFX[0x0258] = "WFng_1st" #600
VFX[0x0259] = "WFng_2nd" #601
VFX[0x01FE] = "SOE_Drm" #510
VFX[0x01FF] = "SOE_Crg" #511
VFX2 = {}
OBJ = []
OBJ.append("BLK_BAS_STAND")# 00
OBJ.append("BLK_BAS_CROUCH")# 01
OBJ.append("BLK_BAS_TURN_STAND")# 02
OBJ.append("BLK_BAS_TURN_CROUCH")# 03
OBJ.append("BLK_BAS_STAND_CROUCH")# 04
OBJ.append("BLK_BAS_CROUCH_STAND")# 05
OBJ.append("BLK_BAS_FORWARD")# 06
OBJ.append("BLK_BAS_BACKWARD")# 07
OBJ.append("BLK_BAS_STAND_JUMP")# 08
OBJ.append("BLK_BAS_JUMP")# 09
OBJ.append("BLK_BAS_JUMP_LAND")# 10
OBJ.append("BLK_BAS_STAND_JUMP_FRONT")# 11
OBJ.append("BLK_BAS_JUMP_FRONT")# 12
OBJ.append("BLK_BAS_JUMP_LAND_FRONT")# 13
OBJ.append("BLK_BAS_STAND_JUMP_BACK")# 14
OBJ.append("BLK_BAS_JUMP_BACK")# 15
OBJ.append("BLK_BAS_JUMP_LAND_BACK")# 16
OBJ.append("BLK_BAS_DASH_FRONT")# 17
OBJ.append("BLK_BAS_DASH_BACK")# 18
OBJ.append("BLK_BAS_DOWN_AOMUKE")# 19
OBJ.append("BLK_BAS_DOWN_UTSUBUSE")# 20
OBJ.append("BLK_BAS_DOWN_STAND_AOMUKE")# 21
OBJ.append("BLK_BAS_DOWN_STAND_UTSUBUSE")# 22
OBJ.append("BLK_BAS_DOWN_STUN_AOMUKE")# 23
OBJ.append("BLK_BAS_DOWN_STUN_UTSUBUSE")# 24
OBJ.append("BLK_BAS_STUN")# 25
OBJ.append("BLK_BAS_NAGE_FAILED")# 26
OBJ.append("BLK_BAS_NAGE_ESCAPE")# 27
OBJ.append("BLK_GRD_START")# 28
OBJ.append("BLK_GRD_START_C")# 29
OBJ.append("BLK_GRD_HL")# 30
OBJ.append("BLK_GRD_HH")# 31
OBJ.append("BLK_GRD_ML")# 32
OBJ.append("BLK_GRD_MH")# 33
OBJ.append("BLK_GRD_LL")# 34
OBJ.append("BLK_GRD_LH")# 35
OBJ.append("BLK_GRD_CL")# 36
OBJ.append("BLK_GRD_CH")# 37
OBJ.append("BLK_GRD_END")# 38
OBJ.append("BLK_GRD_END_C")# 39
OBJ.append("BLK_DMG_HL")# 40
OBJ.append("BLK_DMG_HM")# 41
OBJ.append("BLK_DMG_HH")# 42
OBJ.append("BLK_DMG_ML")# 43
OBJ.append("BLK_DMG_MM")# 44
OBJ.append("BLK_DMG_MH")# 45
OBJ.append("BLK_DMG_LL")# 46
OBJ.append("BLK_DMG_LM")# 47
OBJ.append("BLK_DMG_LH")# 48
OBJ.append("BLK_DMG_UP")# 49
OBJ.append("BLK_DMG_DOWN")# 50
OBJ.append("BLK_DMG_CL")# 51
OBJ.append("BLK_DMG_CM")# 52
OBJ.append("BLK_DMG_CH")# 53
OBJ.append("BLK_DMG_HJ")# 54
OBJ.append("BLK_DMG_HJ_FUKKI")# 55
OBJ.append("BLK_DMG_MJ")# 56
OBJ.append("BLK_DMG_MJ_FUKKI")# 57
OBJ.append("BLK_DMG_TURN_L")# 58
OBJ.append("BLK_DMG_TURN_R")# 59
OBJ.append("BLK_DMG_BLOW")# 60
OBJ.append("BLK_DMG_BLOW_UP")# 61
OBJ.append("BLK_DMG_SPIN")# 62
OBJ.append("BLK_DMG_ASHIBARAI")# 63
OBJ.append("BLK_DMG_BLOW_BOUND")# 64
OBJ.append("BLK_DMG_BOUND_S_AOMUKE")# 65
OBJ.append("BLK_DMG_BOUND_L_AOMUKE")# 66
OBJ.append("BLK_DMG_BOUND_S_UTSUBUSE")# 67
OBJ.append("BLK_DMG_BOUND_L_UTSUBUSE")# 68
OBJ.append("BLK_DMG_WALL_BLOW")# 69
OBJ.append("BLK_DMG_WALL_BOUND_KUZURE")# 70
OBJ.append("BLK_DMG_WALL_BOUND_UTSUBUSE")# 71
OBJ.append("DSM_DMG_BURN_CROUCH")# 72
OBJ.append("RYU_DMG_BURN_JUMP")# 73
OBJ.append("BLK_DMG_BOUND_S_AOMUKE")# 74
OBJ.append("BLK_DMG_KUZURE_STAND")# 75
OBJ.append("BLK_DMG_KUZURE_CROUCH")# 76
OBJ.append("BLK_ATK_5LP")# 77
OBJ.append("BLK_ATK_5LPF")# 78
OBJ.append("BLK_ATK_5MP")# 79
OBJ.append("BLK_ATK_5MPF")# 80
OBJ.append("BLK_ATK_5HP")# 81
OBJ.append("BLK_ATK_5HPF")# 82
OBJ.append("BLK_ATK_5LK")# 83
OBJ.append("BLK_ATK_5LKF")# 84
OBJ.append("BLK_ATK_5MK")# 85
OBJ.append("BLK_ATK_5MKF")# 86
OBJ.append("BLK_ATK_5HK")# 87
OBJ.append("BLK_ATK_5HKF")# 88
OBJ.append("BLK_ATK_2LP")# 89
OBJ.append("BLK_ATK_2MP")# 90
OBJ.append("BLK_ATK_2HP")# 91
OBJ.append("BLK_ATK_2LK")# 92
OBJ.append("BLK_ATK_2MK")# 93
OBJ.append("BLK_ATK_2HK")# 94
OBJ.append("BLK_ATK_8LP")# 95
OBJ.append("BLK_ATK_9LP")# 96
OBJ.append("BLK_ATK_8MP")# 97
OBJ.append("BLK_ATK_9MP")# 98
OBJ.append("BLK_ATK_8HP")# 99
OBJ.append("BLK_ATK_9HP")# 100
OBJ.append("BLK_ATK_8LK")# 101
OBJ.append("BLK_ATK_9LK")# 102
OBJ.append("BLK_ATK_8MK")# 103
OBJ.append("BLK_ATK_9MK")# 104
OBJ.append("BLK_ATK_8HK")# 105
OBJ.append("BLK_ATK_9HK")# 106
OBJ.append("BLK_ATK_6MP")# 107
OBJ.append("BLK_ATK_3HP")# 108
OBJ.append("BLK_ATK_6LMHK")# 109
OBJ.append("BLK_ATK_4LMHK")# 110
OBJ.append("BLK_ATK_22")# 111
OBJ.append("BLK_ATK_SAVING")# 112
OBJ.append("BLK_NGA_SUKARI")# 113
OBJ.append("BLK_NGA_6")# 114
OBJ.append("BLK_NGD_6")# 115
OBJ.append("BLK_NGA_4")# 116
OBJ.append("BLK_NGD_4")# 117
OBJ.append("BLK_SPA_ROLLING")# 118
OBJ.append("BLK_SPA_ROLLING_HIT")# 119
OBJ.append("BLK_SPA_BACKSTEPROLL")# 120
OBJ.append("BLK_SPA_VERTICAL")# 121
OBJ.append("BLK_SPA_MOUNTEN")# 122
OBJ.append("BLK_SPA_THUNDER")# 123
OBJ.append("BLK_SCA_GROUND")# 124
OBJ.append("BLK_SCA_GROUND_BACK_LOOP")# 125
OBJ.append("BLK_SCA_GROUND_FRONT_LOOP")# 126
OBJ.append("BLK_DEM_START")# 127
OBJ.append("BLK_DEM_START_01_01")# 128
OBJ.append("BLK_DEM_START_01_02")# 129
OBJ.append("BLK_DEM_START_01_03")# 130
OBJ.append("BLK_DEM_WIN_01")# 131
OBJ.append("BLK_DEM_WIN_03")# 132
OBJ.append("BLK_DEM_WIN_04")# 133
OBJ.append("BLK_DEM_WIN_05")# 134
OBJ.append("BLK_DEM_MIDDLE_WIN")# 135
OBJ.append("BLK_DEM_MIDDLE_LOSE_AOMUKE")# 136
OBJ.append("BLK_DEM_MIDDLE_LOSE_UTSUBUSE")# 137
OBJ.append("BLK_DEM_DRAW")# 138
OBJ.append("BLK_DEM_LOSE_02")# 139
OBJ.append("BLK_DEM_RESULT_01_01")# 140
OBJ.append("BLK_DEM_RESULT_01_02")# 141
OBJ.append("BLK_DEM_RESULT_01_03")# 142
OBJ.append("BLK_DEM_RESULT_01_04")# 143
OBJ.append("BLK_DEM_RESULT_01_05")# 144
OBJ.append("BLK_DEM_APPEAL_01")# 145
OBJ.append("BLK_DEM_APPEAL_03")# 146
OBJ.append("BLK_DEM_APPEAL_04")# 147
OBJ.append("BLK_DEM_APPEAL_05")# 148
OBJ.append("BLK_DEM_APPEAL_06")# 149
OBJ.append("BLK_DEM_APPEAL_07")# 150
OBJ.append("BLK_DEM_APPEAL_08")# 151
OBJ.append("BLK_DEM_APPEAL_09")# 152
OBJ.append("BLK_DEM_CONTINUE_01")# 153
OBJ.append("BLK_DEM_CONTINUE_02")# 154
OBJ.append("BLK_DEM_CONTINUE_03")# 155
OBJ.append("BLK_DEM_BOSS_START_STAND")# 156
FCE = []
FCE.append("BLK_FCE_DEFAULT")# 00
FCE.append("BLK_FCE_SPK_A")# 01
FCE.append("BLK_FCE_SPK_I")# 02
FCE.append("BLK_FCE_SPK_U")# 03
FCE.append("BLK_FCE_SPK_E")# 04
FCE.append("BLK_FCE_SPK_O")# 05
FCE.append("BLK_FCE_SPK_N")# 06
FCE.append("BLK_FCE_SPK_A_ACCENT")# 07
FCE.append("BLK_FCE_SPK_I_ACCENT")# 08
FCE.append("BLK_FCE_SPK_U_ACCENT")# 09
FCE.append("BLK_FCE_SPK_E_ACCENT")# 10
FCE.append("BLK_FCE_SPK_O_ACCENT")# 11
FCE.append("BLK_FCE_SPK_N_ACCENT")# 12
FCE.append("BLK_FCE_BHV_BLINK")# 13
FCE.append("_blank")# 14
FCE.append("_blank1")# 15
FCE.append("_blank2")# 16
FCE.append("_blank3")# 17
FCE.append("BLK_FCE_EMO_ANGER")# 18
FCE.append("BLK_FCE_EMO_SHOUT")# 19
FCE.append("BLK_FCE_EMO_HATE")# 20
FCE.append("BLK_FCE_EMO_JOY")# 21
FCE.append("BLK_FCE_EMO_SMILE")# 22
FCE.append("BLK_FCE_EMO_FEAR")# 23
FCE.append("BLK_FCE_EMO_SORROW")# 24
FCE.append("BLK_FCE_EMO_CRY")# 25
FCE.append("BLK_FCE_EMO_SURPRISE")# 26
FCE.append("BLK_FCE_EMO_TIREDNESS")# 27
FCE.append("BLK_FCE_EMO_ATACK")# 28
FCE.append("BLK_FCE_EMO_ATACK_ACCENT")# 29
FCE.append("BLK_FCE_EMO_AWAKENING")# 30
FCE.append("_blank4")# 31
FCE.append("_blank5")# 32
FCE.append("BLK_FCE_DMG_HEAD_R")# 33
FCE.append("BLK_FCE_DMG_HEAD_L")# 34
FCE.append("BLK_FCE_DMG_HEAD_ACCENT_R")# 35
FCE.append("BLK_FCE_DMG_HEAD_ACCENT_L")# 36
FCE.append("BLK_FCE_DMG_UPPER")# 37
FCE.append("BLK_FCE_DMG_UPPER_ACCENT")# 38
FCE.append("BLK_FCE_DMG_BODY")# 39
FCE.append("BLK_FCE_DMG_VOMIT")# 40
FCE.append("BLK_FCE_DMG_THUNDER")# 41
FCE.append("BLK_FCE_DMG_DOWN")# 42
FCE.append("BLK_FCE_DMG_DOWN_ACCENT")# 43
FCE.append("_blank6")# 44
FCE.append("_blank7")# 45
FCE.append("_blank8")# 46
FCE.append("BLK_FCE_UNQ_DEFAULT")# 47
FCE.append("BLK_FCE_UNQ_DEFAULT_L")# 48
FCE.append("BLK_FCE_UNQ_ATACK")# 49
FCE.append("BLK_FCE_UNQ_MENACE")# 50
FCE.append("BLK_FCE_UNQ_FOOTWORK")# 51
CAM = []
CAM.append("BLK_CAM_ULTRA")# 00
CAM.append("BLK_CAM_SHOUT_L")# 01
CAM.append("BLK_CAM_SHOUT_S_01")# 02
CAM.append("BLK_CAM_SHOUT_S_02")# 03
CAM.append("BLK_CAM_START_01_01")# 04
CAM.append("BLK_CAM_START_01_02")# 05
CAM.append("BLK_CAM_START_01_03")# 06
CAM.append("BLK_CAM_RESULT_01_01")# 07
CAM.append("BLK_CAM_RESULT_01_02")# 08
CAM.append("BLK_CAM_RESULT_01_03")# 09
CAM.append("BLK_CAM_RESULT_01_04")# 10
CAM.append("BLK_CAM_RESULT_01_05")# 11
CAM.append("BLK_CAM_CONTINUE_01")# 12
CAM.append("BLK_CAM_CONTINUE_02")# 13
CAM.append("BLK_CAM_CONTINUE_03")# 14
CAM.append("BLK_CAM_RIVAL_01_01")# 15
CAM.append("BLK_CAM_RIVAL_01_02")# 16
CAM.append("BLK_CAM_RIVAL_01_03")# 17
CAM.append("BLK_CAM_RIVAL_01_04")# 18
CAM.append("BLK_CAM_RIVAL_01_05")# 19
CAM.append("BLK_CAM_RIVAL_01_06")# 20
CAM.append("BLK_CAM_RIVAL_01_07")# 21
CAM.append("BLK_CAM_RIVAL_01_08")# 22
UC1 = []
UC1.append("BLK_UCA_ULTRA")# 00
UC1.append("BLK_UCD_ULTRA")# 01
UC2 = []
UC2.append("BLK_UCA_SHOUT_L")# 00
UC2.append("BLK_UCA_SHOUT_S_01")# 01
UC2.append("BLK_UCA_SHOUT_S_02")# 02
