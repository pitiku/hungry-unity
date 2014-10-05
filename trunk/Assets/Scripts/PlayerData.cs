using UnityEngine;
using System.Collections;

public class PlayerData
{
	public const int NUM_BABIES = 16;
	public enum BABIES
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

	//Options
	public static bool music;
	public static bool sound;
	public static bool accelerometer;

	//Stats
	public static int maxBabies;
	public static int totalBabies;
	public static int maxCoinsInAGame;
	public static int coins;
	public static int totalCoins;
	public static int spentCoins;
	public static int numGames;
	public static int numPrizes;
	public static int maxCombo;

	public static int lastDayPlayed;

	//Items
	public static bool upgrade_accelerometer;
	public static bool upgrade_crown;
	public static bool upgrade_globes;
	public static bool upgrade_rainbowplus;
	public static bool upgrade_rainbowplusplus;
	public static bool upgrade_vacuum;

	public static int powerup_boletTime;
	public static int powerup_chainBoost;
	public static int powerup_doubleCoins;
	public static int powerup_extraRainbow;
	public static int powerup_megaChainBoost;
	public static int powerup_prizeSeason;

	//Babies
	public static bool[] babies;

	//Achievements
	public static bool[] achievements;
	
	public static void Load()
	{
		music = GetBool("music");
		sound = GetBool("sound");
		accelerometer = GetBool("accelerometer");

		maxBabies = PlayerPrefs.GetInt("maxBabies");
		totalBabies = PlayerPrefs.GetInt("totalBabies");
		maxCoinsInAGame = PlayerPrefs.GetInt("maxCoinsInAGame");
		coins = PlayerPrefs.GetInt("coins");
		totalCoins = PlayerPrefs.GetInt("totalCoins");
		spentCoins = PlayerPrefs.GetInt("spentCoins");
		numGames = PlayerPrefs.GetInt("numGames");
		numPrizes = PlayerPrefs.GetInt("numPrizes");
		maxCombo = PlayerPrefs.GetInt("maxCombo");

		lastDayPlayed = PlayerPrefs.GetInt("lastDayPlayed");

		upgrade_accelerometer = GetBool("upgrade_accelerometer");
		upgrade_crown = GetBool("upgrade_crown");
		upgrade_globes = GetBool("upgrade_globes");
		upgrade_rainbowplus = GetBool("upgrade_rainbowplus");
		upgrade_rainbowplusplus = GetBool("upgrade_rainbowplusplus");
		upgrade_vacuum = GetBool("upgrade_vacuum");

		powerup_boletTime = PlayerPrefs.GetInt("powerup_boletTime");
		powerup_chainBoost = PlayerPrefs.GetInt("powerup_chainBoost");
		powerup_doubleCoins = PlayerPrefs.GetInt("powerup_doubleCoins");
		powerup_extraRainbow = PlayerPrefs.GetInt("powerup_extraRainbow");
		powerup_megaChainBoost = PlayerPrefs.GetInt("powerup_megaChainBoost");
		powerup_prizeSeason = PlayerPrefs.GetInt("powerup_prizeSeason");

		babies = new bool[NUM_BABIES];
		for(int i=0; i < NUM_BABIES; ++i)
		{
			babies[i] = GetBool("baby"+i);
		}
	}

	public static void Save()
	{
		SetBool("music",  music);
		SetBool("sound", sound);
		SetBool("accelerometer", accelerometer);
		
		PlayerPrefs.SetInt("maxBabies", maxBabies);
		PlayerPrefs.SetInt("totalBabies", totalBabies);
		PlayerPrefs.SetInt("maxCoinsInAGame", maxCoinsInAGame);
		PlayerPrefs.SetInt("coins", coins);
		PlayerPrefs.SetInt("totalCoins", totalCoins);
		PlayerPrefs.SetInt("spentCoins", spentCoins);
		PlayerPrefs.SetInt("numGames", numGames);
		PlayerPrefs.SetInt("numPrizes", numPrizes);
		PlayerPrefs.SetInt("maxCombo", maxCombo);
		
		PlayerPrefs.SetInt("lastDayPlayed", lastDayPlayed);
		
		SetBool("upgrade_accelerometer", upgrade_accelerometer);
		SetBool("upgrade_crown", upgrade_crown);
		SetBool("upgrade_globes", upgrade_globes);
		SetBool("upgrade_rainbowplus", upgrade_rainbowplus);
		SetBool("upgrade_rainbowplusplus", upgrade_rainbowplusplus);
		SetBool("upgrade_vacuum", upgrade_vacuum);
		
		PlayerPrefs.SetInt("powerup_boletTime", powerup_boletTime);
		PlayerPrefs.SetInt("powerup_chainBoost", powerup_chainBoost);
		PlayerPrefs.SetInt("powerup_doubleCoins", powerup_doubleCoins);
		PlayerPrefs.SetInt("powerup_extraRainbow", powerup_extraRainbow);
		PlayerPrefs.SetInt("powerup_megaChainBoost", powerup_megaChainBoost);
		PlayerPrefs.SetInt("powerup_prizeSeason", powerup_prizeSeason);

		for(int i=0; i < NUM_BABIES; ++i)
		{
			SetBool("baby"+i, babies[i]);
		}

		PlayerPrefs.Save();
	}

	public static bool AnyInitialPowerUp()
	{
		return powerup_boletTime > 0 || powerup_chainBoost > 0 || powerup_doubleCoins > 0 || powerup_megaChainBoost > 0 || powerup_prizeSeason > 0;
	}
	
	public static bool AnyFinalPowerUp()
	{
		return powerup_extraRainbow > 0;
	}
	
	public static void SetBool(string _key, bool _value)
	{
		PlayerPrefs.SetInt(_key, _value ? 1 : 0);
	}

	public static bool GetBool(string _key)
	{
		if(!PlayerPrefs.HasKey(_key)) return false;
		return PlayerPrefs.GetInt(_key)==1 ? true : false;
	}
}
