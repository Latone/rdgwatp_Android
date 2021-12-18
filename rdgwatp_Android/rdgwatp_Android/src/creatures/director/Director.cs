using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.creatures.types;
using ConsoleApp1.src.items;

namespace ConsoleApp1.src.creatures.director
{
    //Director - конструирует существ
    class Director
    {
        /*short, byte - всё в (процентах/100) 
	 * for an example: 1 - 1 процент
	 * 				   150 - в полтора раза больше и т.д.*/
        //setPercetageOfTimeThinks:
        //How it's done: Chance = i/(4+i)
        //Далее идущие цифры идут как максимумы в диапазоне: [1;m), где m - цифра
        //6 - 20%
        //7 - (1/3)%
        //8 - 42%
        //9 - 50%
        //And else...
        public void constructPlayer(CreatureBuilder cb, short x, short y)
        {
            cb.setType(types.Type.PLAYER); //Много типов из-за неоднозначности. Так и в последующих
            cb.setHP((short)100);
            cb.setMaxHP((short)100);
            cb.setARMOR((short)5);
            cb.setCh_Evade((byte)5);
            cb.setCh_Block((byte)0); //Щита нет, увы + это не парирование
            cb.setDMG((short)7);
            cb.setDefaultDMG((short)7);
            cb.setCh_Crit_Attack((byte)10);
            cb.setPower_Crit_Attack((short)125);
            cb.setX(x);
            cb.setY(y);
            cb.setIcon('P');//1CC3
            cb.setInventory(CreateBasicInventory.getBasicInventory(0)); //Значение в методе getBasicInvetory означает сокращение максимума предметов на ..
        }
        public void constructSkeleton(CreatureBuilder cb, short x, short y)
        {
            cb.setType(types.Type.EN_SKELETON);
            cb.setHP((short)20);
            cb.setMaxHP((short)20);
            cb.setARMOR((short)10);
            cb.setCh_Evade((byte)5);
            cb.setCh_Block((byte)0);
            cb.setDMG((short)5);
            cb.setDefaultDMG((short)5);
            cb.setCh_Crit_Attack((byte)5);
            cb.setPower_Crit_Attack((short)125);
            cb.setX(x);
            cb.setY(y);
            cb.setIcon('\x2620');//25C6
            cb.setChase(false);
            cb.setPercentageOfTimeThinks(7);
            cb.setNoticeRange(3);
            cb.setVanishRange(5);
            cb.setChasePath(null);
            cb.setInventory(CreateBasicInventory.getBasicInventory(2)); //Значение в методе getBasicInvetory означает сокращение максимума предметов на ..
            cb.setEmotions(new String[] {         "╔══════════════════════╗\n"+
                                                  "║                  ,-'\"\"'-.                 ║\n"+
                                                  "║                 ;        :                ║\n"+
                                                  "║                :          :               ║\n"+
                                                  "║                :  _    _  ;               ║\n"+
                                                  "║                : ( )  ( ) :               ║\n"+
                                                  "║                ::   '`   :;               ║\n"+
                                                  "║                 !:      :!                ║\n"+
                                                  "║                 `:`++++';'                ║\n"+
                                                  "║                   `....'                  ║\n"+
                                                  "║                                           ║\n"+
                                                  "║                  SKELETON                 ║\n"+
                                                  "║                                           ║\n"+
                                                  "║                                           ║\n"+
                                                  "╚══════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║                                           ║\n"+
                                                  "║                  ,-'\"\"'-.                 ║\n"+
                                                  "║                 ;  ▚     :                ║\n"+
                                                  "║                :  ▚▓      ;               ║\n"+
                                                  "║                :  ⦣   ∠   :               ║\n"+
                                                  "║                ::   '`   :;               ║\n"+
                                                  "║                 !:/!++!\\:!                ║\n"+
                                                  "║                 `:`-++-';'                ║\n"+
                                                  "║                   `....'                  ║\n"+
                                                  "║                                           ║\n"+
                                                  "║                  SKELETON                 ║\n"+
                                                  "║                                           ║\n"+
                                                  "║                                           ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║    ,-\\\\__\\      ,-'\"\"'-.                  ║\n"+
                                                  "║    |f-\"Y\\|     ;  _     :                 ║\n"+
                                                  "║    \\()7L/      : )#(     :                ║\n"+
                                                  "║     cgD       :          ;                ║\n"+
                                                  "║     |\\(       :  ಠ   ಠ   :                ║\n"+
                                                  "║      \\\\       ::   '`   :;                ║\n"+
                                                  "║       \\\\       !:_    _:!                 ║\n"+
                                                  "║        \\\\_____ `:`++++';'                 ║\n"+
                                                  "║         с7__-__)(`....'                   ║\n"+
                                                  "║                  \\                        ║\n"+
                                                  "║                  SKELETON                 ║\n"+
                                                  "║                                           ║\n"+
                                                  "║                                           ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║           _     ,-,    _                  ║\n" +
                                                  "║     ,--, /: :\\/': :`\\/: :\\                ║\n" +
                                                  "║    \\`; '˄`,'  ˄`.;  ˄ `: /    ,-'\"'-.     ║\n" +
                                                  "║     |  | |   | |'  | |  |.   ;)#(    :    ║\n" +
                                                  "║     | :| |   | |pb | |  ||  :  ◣   ◢  :   ║\n" +
                                                  "║    / :. ˅  : | |:  | |  |   :    '`   :   ║\n" +
                                                  "║    \\__ /: :.. ˅: :..˅ :. \\) :         :   ║\n" +
                                                  "║          `---',\\___/,\\___/ /':_/!++!\\!    ║\n" +
                                                  "║                     `==._.. ./ `-++-';    ║\n" +
                                                  "║                         `-::-'`.....`     ║\n" +
                                                  "║                  SKELETON                 ║\n" +
                                                  "║                                           ║\n" +
                                                  "║                                           ║\n" +
                                                  "╚═══════════════════════════════════════════╝"});



        }
        public void constructSlime(CreatureBuilder cb, short x, short y)
        {
            cb.setType(types.Type.EN_SLIME);
            cb.setHP((short)15);
            cb.setMaxHP((short)15);
            cb.setARMOR((short)3);
            cb.setCh_Evade((byte)15);
            cb.setCh_Block((byte)5); //Щита есть, увы. Щит в желе. ** P.S. Оставил прошлый боец
            cb.setDMG((short)5);
            cb.setDefaultDMG((short)5);
            cb.setCh_Crit_Attack((byte)7);
            cb.setPower_Crit_Attack((short)125);
            cb.setX(x);
            cb.setY(y);
            cb.setIcon('۝'); //or this one 0DA9
            cb.setChase(false);
            cb.setPercentageOfTimeThinks(7);
            cb.setNoticeRange(3);
            cb.setVanishRange(4);
            cb.setChasePath(null);
            cb.setInventory(CreateBasicInventory.getBasicInventory(2)); //Значение в методе getBasicInvetory означает сокращение максимума предметов на ..
            cb.setEmotions(new String[] {         "╔═══════════════════════════════════════════╗\n"+
                                                  "║                ██████████                 ║\n"+
                                                  "║            ████▒▒▒▒░░░░░░████             ║\n"+
                                                  "║          ██▒▒▒▒░░░░░░      ░░██           ║\n"+
                                                  "║          ██▒▒▒▒░░░░░░░░    ░░██           ║\n"+
                                                  "║        ██▒▒▒▒▒▒░░░░░░░░░░░░░░▒▒██         ║\n"+
                                                  "║        ██▒▒▒▒▒▒▒▒░░░░░░░░░░░░▒▒██         ║\n"+
                                                  "║        ██▒▒▒▒▒▒▒▒▒▒░░░░░░░░▒▒▒▒██         ║\n"+
                                                  "║          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██           ║\n"+
                                                  "║            ██████████████████             ║\n"+
                                                  "║                  Slime                    ║\n"+
                                                  "║                                           ║\n"+
                                                  "║                                           ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║                ███████     █████          ║\n"+
                                                  "║            ████▒  ▒░░█   ██░░░░█          ║\n"+
                                                  "║          ██▒▒░ ░░░▒░░ ███░  ░ ██          ║\n"+
                                                  "║            ██▒░▒ ░░░   ▒░░    ░░██        ║\n"+
                                                  "║            ██▒ ▒░ ▒░░ ░░▒▒▒░░░░░░██       ║\n"+
                                                  "║           ██░▒ ▒ ░▒▒░░  ░░░░░░░▒▒██       ║\n"+
                                                  "║          ██░ ░░░░▒▒▒░░ ░░░░░▒▒▒▒██        ║\n"+
                                                  "║          ██▒▒░ ░███▒▒▒▒▒░  ▒▒▒▒█          ║\n"+
                                                  "║            █████   ████▒▒▒▒▒▒░░█          ║\n"+
                                                  "║                  Slime ██████▒█           ║\n"+
                                                  "║                              █            ║\n"+
                                                  "║                                           ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║   ███                          ███████    ║\n"+
                                                  "║  █▒  █                        █▒▒▒    █   ║\n"+
                                                  "║  █▒   █      ██████████   ████░░░░░   ▒█  ║\n"+
                                                  "║  █░░░░▒██████▒▒░░░░░░░░███▒░░▒░░░░░░░░▒█  ║\n"+
                                                  "║   █░░░░░▒▒▒▒▒▒▒░░░░░░░░░░░░░░▒▒██▒░░░░▒█  ║\n"+
                                                  "║    ██▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░░▒▒█▒▒▒▒▒█    ║\n"+
                                                  "║      ████▒▒▒▒▒▒▒▒▒▒░░░░░░░░▒▒▒▒▒▒▒▒▒█     ║\n"+
                                                  "║          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒███████      ║\n"+
                                                  "║            ██████████████████             ║\n"+
                                                  "║                  Slime                    ║\n"+
                                                  "║                                           ║\n"+
                                                  "║                                           ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║██████████████         █████████████████   ║\n"+
                                                  "║▒▒▒▒▒▒▒▒▒▒▒████       █░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██ ║\n"+
                                                  "║▒▒▒▒▒██▒▒▒▒▒██████████▒▒▒▒▒▒██████▒▒▒▒▒▒▒▒█║\n"+
                                                  "║▒▒▒▒▒▒▒▒▒▒▒▒████░░░░█░▒▒▒▒████████████▒▒▒▒█║\n"+
                                                  "║▒▒▒██▒▒▒▒▒▒▒▒▒▒█░██░█▒▒▒▒██████████▒▒▒▒▒▒▒▒║\n"+
                                                  "║▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██  ██▒▒▒▒▒██████████▒▒▒▒▒▒▒║\n"+
                                                  "║▒▒▒████▒▒▒▒▒▒▒▒▒█    █░▒▒▒▒▒▒████▒▒▒▒▒▒██▒▒║\n"+
                                                  "║▒████████▒▒▒▒▒▒▒█    █▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒║\n"+
                                                  "║▒▒▒████▒▒▒▒▒▒▒▒▒██    ██▒▒▒████▒▒▒▒▒▒▒▒▒▒▒▒║\n"+
                                                  "║▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█ Slime█▒▒▒██▒▒▒▒▒▒▒▒▒▒▒▒▒▒║\n"+
                                                  "║▒▒▒▒▒▒▒▒▒████████        █▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒║\n"+
                                                  "║▒▒▒▒▒▒▒▒█                 █▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒ ║\n"+
                                                  "╚═══════════════════════════════════════════╝",});
        }
        public void constructDoorKnob(CreatureBuilder cb, short x, short y)
        {
            cb.setType(types.Type.EN_DOOR_KNOB);
            cb.setHP((short)10);
            cb.setMaxHP((short)10);
            cb.setARMOR((short)20);
            cb.setCh_Evade((byte)0);
            cb.setCh_Block((byte)50); //Дверь и есть щит
            cb.setDMG((short)2);
            cb.setDefaultDMG((short)2);
            cb.setCh_Crit_Attack((byte)1);
            cb.setPower_Crit_Attack((short)125);
            cb.setX(x);
            cb.setY(y);
            cb.setIcon('⧱');
            cb.setChase(false);
            cb.setPercentageOfTimeThinks(7);
            cb.setNoticeRange(1);
            cb.setVanishRange(2);
            cb.setChasePath(null);
            cb.setDisableTime(55);
            cb.setInventory(CreateBasicInventory.getBasicInventory(3)); //Значение в методе getBasicInvetory означает сокращение максимума предметов на ..
            cb.setEmotions(new String[] {         "╔═══════════════════════════════════════════╗\n"+
                                                  "║                ______________             ║\n"+
                                                  "║              |\\ ___________ /|            ║\n"+
                                                  "║              | |  _ _ _ _  | |            ║\n"+
                                                  "║              | | | | | | | | |            ║\n"+
                                                  "║              | | |-+-+-+-| | |            ║\n"+
                                                  "║              | | |-+-+=+%| | |            ║\n"+
                                                  "║              | | |_|_|_|_| | |            ║\n"+
                                                  "║              | |    ___    | |            ║\n"+
                                                  "║              | |   [___] ()| |            ║\n"+
                                                  "║              | |         ||| |            ║\n"+
                                                  "║              | |         ()| |            ║\n"+
                                                  "║              | |           | |            ║\n"+
                                                  "║              | |           | |            ║\n"+
                                                  "║              | |           | |            ║\n"+
                                                  "║  door_knob   |_|___________|_|            ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║                ______________             ║\n"+
                                                  "║              |\\ ___________ /|            ║\n"+
                                                  "║              | |  /|,| |   | |            ║\n"+
                                                  "║              | | |,x,| |   | |            ║\n"+
                                                  "║              | | |,x,' |   | |            ║\n"+
                                                  "║              | | |,x   ,   | |            ║\n"+
                                                  "║              | | |/   ()   | |            ║\n"+
                                                  "║              | |    /] ,   | |            ║\n"+
                                                  "║              | |   [/ ()   | |            ║\n"+
                                                  "║              | |       |   | |            ║\n"+
                                                  "║              | |       |   | |            ║\n"+
                                                  "║              | |       |   | |            ║\n"+
                                                  "║              | |      ,'   | |            ║\n"+
                                                  "║              | |   ,'      | |            ║\n"+
                                                  "║  door_knob   |_|,'_________|_|            ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║    ~~~        ______________    ~~        ║\n"+
                                                  "║              |\\ ___________ /|            ║\n"+
                                                  "║              | |  _~_ _ _  | |      ゴ     ║\n"+
                                                  "║       ゴ    ~ | | | | | | | | |     ゴ      ║\n"+
                                                  "║      ゴ       | | |-+-+-+-| | |   ゴ        ║\n"+
                                                  "║     ゴ        | | |-+-+=+%| | |     ゴ      ║\n"+
                                                  "║          ゴ   | | |_|_|_|_| | |    ゴ       ║\n"+
                                                  "║      ~~ ゴ    | |    ___    | |   ~~~      ║\n"+
                                                  "║              | |   [___] ()| |            ║\n"+
                                                  "║    ~~~~~~    | |         ||| |            ║\n"+
                                                  "║              | |  ~~~    ()| |            ║\n"+
                                                  "║         ゴ    | |     ゴ     | |        ゴ   ║\n"+
                                                  "║      ゴ       | |   ゴ    ~~ | |       ゴ    ║\n"+
                                                  "║              | | ~~        | |      ゴ     ║\n"+
                                                  "║  door_knob   |_|___________|_|            ║\n"+
                                                  "╚═══════════════════════════════════════════╝",
                                                  "╔═══════════════════════════════════════════╗\n"+
                                                  "║                ______________             ║\n"+
                                                  "║              |\\ ___________ /|           ║\n"+
                                                  "║              | |  _ _ _ _  | |            ║\n"+
                                                  "║              | | | | | | | | |            ║\n"+
                                                  "║              | | |-+-+-+-| | |            ║\n"+
                                                  "║              | | |-+-+=+%| | |  *NOCK-    ║\n"+
                                                  "║              | | |_|_|_|_| | |   NOCK*    ║\n"+
                                                  "║              | |    ___    | |            ║\n"+
                                                  "║-WHO IS THERE?| |   [___] ()| |            ║\n"+
                                                  "║              | |         ||| |            ║\n"+
                                                  "║              | |         ()| |*SCREECHING*║\n"+
                                                  "║              | |           | |-A BAD JOKE!║\n"+
                                                  "║              | |           | |            ║\n"+
                                                  "║              | |           | |            ║\n"+
                                                  "║  door_knob   |_|___________|_|            ║\n"+
                                                  "╚═══════════════════════════════════════════╝",});
        }
        public void constructGhost(CreatureBuilder cb, short x, short y)
        {
            cb.setType(types.Type.ENT_GHOST); //Много типов из-за неоднозначности. Так и в последующих
            cb.setHP((short)1);
            cb.setMaxHP((short)1);
            cb.setARMOR((short)0);
            cb.setCh_Evade((byte)100);
            cb.setCh_Block((byte)0); //Щита нет, увы + это не парирование
            cb.setDMG((short)5);
            cb.setDefaultDMG((short)5);
            cb.setCh_Crit_Attack((byte)100);
            cb.setPower_Crit_Attack((short)125);
            cb.setX(x);
            cb.setY(y);
            cb.setIcon('⻤'); //or this one 0DA9
            cb.setChase(false);
            cb.setPercentageOfTimeThinks(7);
            cb.setNoticeRange(1);
            cb.setVanishRange(2);
            cb.setChasePath(null);
        }
    }
}
