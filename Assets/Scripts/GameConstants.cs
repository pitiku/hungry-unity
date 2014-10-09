using UnityEngine;
using System.Collections;

public class GameConstants : MonoBehaviour {
	
	public const int NUM_BABIES = 16;
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
		CHAMELEON
	};
	
	public enum eUpgrades
	{
		ACCELEROMETER = 0,
		CROWN,
		SUPER_RAINBOW,
		VACUUM,
		GLOVES,
		MEGA_RAINBOW
	};
}
