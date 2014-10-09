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

	public bool loaded = false;

	public int lastDayPlayed = 0;

	//Options
	public bool option_music = true;
	public bool option_sound = true;
	public bool option_accelerometer = false;

	//Stats
	public int maxBabies = 0;
	public int totalBabies = 0;
	public int maxCoinsInAGame = 0;
	private int coins = 0;
	public int totalCoins = 0;
	public int spentCoins = 0;
	public int numGames = 0;
	public int numPrizes = 0;
	public int maxCombo = 0;
	
	//Items
	public bool upgrade_accelerometer = false;
	public bool upgrade_crown = false;
	public bool upgrade_gloves = false;
	public bool upgrade_rainbowplus = false;
	public bool upgrade_rainbowplusplus = false;
	public bool upgrade_vacuum = false;

	public int powerup_boletTime = 0;
	public int powerup_chainBoost = 0;
	public int powerup_doubleCoins = 0;
	public int powerup_extraRainbow = 0;
	public int powerup_megaChainBoost = 0;
	public int powerup_prizeSeason = 0;

	//Babies
	public bool[] babies;

	public int Coins
	{
		get
		{
			return coins;
		}
		set
		{
			coins = value;
			if(CoinsCounter.Instance)
			{
				CoinsCounter.Instance.UpdateCoins();
			}
		}
	}
	
	public void Load()
	{
		if(loaded)
		{
			return;
		}

		loaded = true;

		//Uncomment to reset
		//PlayerPrefs.DeleteKey("music");
		
		//Check is there is saved data
		if(PlayerPrefs.HasKey("music"))
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

			babies = new bool[(int)GameConstants.eBabies.NUM_BABIES];
			for(int i=0; i < (int)GameConstants.eBabies.NUM_BABIES; ++i)
			{
				babies[i] = GetBool("baby"+i);
			}
		}
		else
		{
			//Unlock default babies
			babies = new bool[(int)GameConstants.eBabies.NUM_BABIES];
			babies[(int)GameConstants.eBabies.HUMAN] = true;
			babies[(int)GameConstants.eBabies.CAT] = true;
			babies[(int)GameConstants.eBabies.DOG] = true;
			babies[(int)GameConstants.eBabies.BIRD] = true;

			Save();
		}
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

		for(int i=0; i < (int)GameConstants.eBabies.NUM_BABIES; ++i)
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
		return PlayerPrefs.GetInt(_key)==1 ? true : false;
	}
}
