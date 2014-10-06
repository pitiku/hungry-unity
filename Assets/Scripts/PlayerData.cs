using UnityEngine;
using System.Collections;

public class PlayerData
{
	private static PlayerData _Instance = null;
	public static PlayerData Instance
	{
		get
		{
			if(_Instance == null)
			{
				_Instance = new PlayerData();
			}
			return _Instance;
		}
	}

	private PlayerData()
	{
		Load();
	}

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

	public bool loaded = false;

	//Options
	public bool option_music;
	public bool option_sound;
	public bool option_accelerometer;

	//Stats
	public int maxBabies;
	public int totalBabies;
	public int maxCoinsInAGame;
	private int coins;
	public int totalCoins;
	public int spentCoins;
	public int numGames;
	public int numPrizes;
	public int maxCombo;

	public int lastDayPlayed;

	//Items
	public bool upgrade_accelerometer;
	public bool upgrade_crown;
	public bool upgrade_gloves;
	public bool upgrade_rainbowplus;
	public bool upgrade_rainbowplusplus;
	public bool upgrade_vacuum;

	public int powerup_boletTime;
	public int powerup_chainBoost;
	public int powerup_doubleCoins;
	public int powerup_extraRainbow;
	public int powerup_megaChainBoost;
	public int powerup_prizeSeason;

	//Babies
	public bool[] babies;

	//Achievements
	public bool[] achievements;
		
	public int Coins
	{
		get
		{
			return coins;
		}
		set
		{
			coins = value;
			CoinsCounter.Instance.UpdateCoins();
		}
	}
	
	private void Load()
	{
		option_music = GetBool("music");
		option_sound = GetBool("sound");
		option_accelerometer = GetBool("accelerometer");

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
		upgrade_gloves = GetBool("upgrade_globes");
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

		loaded = true;
	}

	public void Save()
	{
		if(!loaded)
		{
			Load();
		}
		SetBool("music",  option_music);
		SetBool("sound", option_sound);
		SetBool("accelerometer", option_accelerometer);
		
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
		SetBool("upgrade_globes", upgrade_gloves);
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

	public bool AnyInitialPowerUp()
	{
		return powerup_boletTime > 0 || powerup_chainBoost > 0 || powerup_doubleCoins > 0 || powerup_megaChainBoost > 0 || powerup_prizeSeason > 0;
	}
	
	public bool AnyFinalPowerUp()
	{
		return powerup_extraRainbow > 0;
	}
	
	public void SetBool(string _key, bool _value)
	{
		PlayerPrefs.SetInt(_key, _value ? 1 : 0);
	}

	public bool GetBool(string _key)
	{
		if(!PlayerPrefs.HasKey(_key)) return false;
		return PlayerPrefs.GetInt(_key)==1 ? true : false;
	}
}
