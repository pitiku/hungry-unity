using UnityEngine;
using System.Collections;

public class GameConstants : MonoBehaviour {
	
	public enum eBabies
	{
		HUMAN = 0,
		DOG,
		CAT,
		BIRD,
		MONKEY,
		WHALE,
		ALIEN,
		ANTEATER,
		PLANT,
		RABBIT,
		ROBOT,
		ZOMBIE,
		PANDA,
		COW,
		BAT,
		CHAMELEON,
		NUM_BABIES
	};
	
	public enum eUpgrades
	{
		ACCELEROMETER = 0,
		CROWN,
		SUPER_RAINBOW,
		VACUUM,
		GLOVES,
		MEGA_RAINBOW,
		NUM_UPGRADES
	};

	public enum ePowerups
	{
		BOLET_TIME = 0,
		DOUBLE_COINS,
		EXTRA_RAINBOW,
		PRIZE_SEASON,
		CHAIN_BOOST,
		MEGA_CHAIN_BOOST,
		NUM_POWERUPS
	};
}
