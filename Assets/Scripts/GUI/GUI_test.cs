using UnityEngine;
using System.Collections;

public class GUI_test : MonoBehaviour {

	public Texture2D iconWepT0;
	public Texture2D iconWepT1;
	public Texture2D iconWepT2;
	public Texture2D iconWepT3;
	public Texture2D iconArmorT0;
	public Texture2D iconArmorT1;
	public Texture2D iconArmorT2;
	public Texture2D iconArmorT3;
	public Texture2D iconBootsT0;
	public Texture2D iconBootsT1;
	public Texture2D iconBootsT2;
	public Texture2D iconBootsT3;
	public Texture2D iconGatT0;
	public Texture2D iconGatT1;
	public Texture2D iconGatT2;
	public Texture2D iconGatT3;
	public Texture2D iconMetalT1;
	public Texture2D iconMetalT2;
	public Texture2D iconMetalT3;
	public Texture2D iconTextilT1;
	public Texture2D iconTextilT2;
	public Texture2D iconTextilT3;
	public Texture2D iconElectronicT1;
	public Texture2D iconElectronicT2;
	public Texture2D iconElectronicT3;
	public GameObject Player1;
	public GameObject Player2;
	public Texture2D redBG, blueBG;

	private Texture2D[] inv = new Texture2D[10];
	private Texture2D[] inv2 = new Texture2D[10];

	private Texture2D[] equipP1 = new Texture2D[4];
	private Texture2D[] equipP2 = new Texture2D[4];

	private GUIStyle currentStyle = null;
	private GUIStyle currentStyle2 = null;

	private void InitStyles()	
	{
		if( currentStyle == null )	
		{
			currentStyle = new GUIStyle( GUI.skin.box );
			currentStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 1f, 0f, 0.5f ) );
		}
		if( currentStyle2 == null )	
		{
			currentStyle2 = new GUIStyle( GUI.skin.box );
			currentStyle2.normal.background = MakeTex( 2, 2, new Color( 0f, 0f, 0f, 1f ) );
		}
	}
	
	private Texture2D MakeTex( int width, int height, Color col )	
	{
		
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )	
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

	string GetDurability(float durabilityRatio){
		if (float.IsPositiveInfinity (durabilityRatio)) {
			return "";
		} else {
			return Mathf.RoundToInt(durabilityRatio*100) + " %";
		}
	}

	void OnGUI () {
		InitStyles();

		PlayerInventory inventoryP1;
		inventoryP1 = Player1.GetComponent<PlayerInventory>();
		PlayerInventory inventoryP2;
		inventoryP2 = Player2.GetComponent<PlayerInventory>();
		PlayerEquipment equipementP1;
		equipementP1 = Player1.GetComponent<PlayerEquipment>();
		PlayerEquipment equipementP2;
		equipementP2 = Player2.GetComponent<PlayerEquipment>();

		PlayerCombat PC1 = Player1.GetComponent<PlayerCombat>();
		PlayerCombat PC2 = Player2.GetComponent<PlayerCombat>();

		float pct1, pct2;
		pct1 = (float)PC1.hp / (float)PC1.maxhp;
		pct2 = (float)PC2.hp / (float)PC2.maxhp;

		for (int i = 0; i < 10; i++) {
			if(inventoryP1.inventaire[i].nom == "Metal" && inventoryP1.inventaire[i].tier == 1){
				inv[i] = iconMetalT1;
			}else if(inventoryP1.inventaire[i].nom == "Metal" && inventoryP1.inventaire[i].tier == 2){
				inv[i] = iconMetalT2;
			}else if(inventoryP1.inventaire[i].nom == "Metal" && inventoryP1.inventaire[i].tier == 3){
				inv[i] = iconMetalT3;
			}else if(inventoryP1.inventaire[i].nom == "Textile" && inventoryP1.inventaire[i].tier == 1){
				inv[i] = iconTextilT1;
			}else if(inventoryP1.inventaire[i].nom == "Textile" && inventoryP1.inventaire[i].tier == 2){
				inv[i] = iconTextilT2;
			}else if(inventoryP1.inventaire[i].nom == "Textile" && inventoryP1.inventaire[i].tier == 3){
				inv[i] = iconTextilT3;
			}else if(inventoryP1.inventaire[i].nom == "Electronique" && inventoryP1.inventaire[i].tier == 1){
				inv[i] = iconElectronicT1;
			}else if(inventoryP1.inventaire[i].nom == "Electronique" && inventoryP1.inventaire[i].tier == 2){
				inv[i] = iconElectronicT2;
			}else if(inventoryP1.inventaire[i].nom == "Electronique" && inventoryP1.inventaire[i].tier == 3){
				inv[i] = iconElectronicT3;
			}else{
				inv[i] = null;
			}

			/// Player 2
			 
			if(inventoryP2.inventaire[i].nom == "Metal" && inventoryP2.inventaire[i].tier == 1){
				inv2[i] = iconMetalT1;
			}else if(inventoryP2.inventaire[i].nom == "Metal" && inventoryP2.inventaire[i].tier == 2){
				inv2[i] = iconMetalT2;
			}else if(inventoryP2.inventaire[i].nom == "Metal" && inventoryP2.inventaire[i].tier == 3){
				inv2[i] = iconMetalT3;
			}else if(inventoryP2.inventaire[i].nom == "Textile" && inventoryP2.inventaire[i].tier == 1){
				inv2[i] = iconTextilT1;
			}else if(inventoryP2.inventaire[i].nom == "Textile" && inventoryP2.inventaire[i].tier == 2){
				inv2[i] = iconTextilT2;
			}else if(inventoryP2.inventaire[i].nom == "Textile" && inventoryP2.inventaire[i].tier == 3){
				inv2[i] = iconTextilT3;
			}else if(inventoryP2.inventaire[i].nom == "Electronique" && inventoryP2.inventaire[i].tier == 1){
				inv2[i] = iconElectronicT1;
			}else if(inventoryP2.inventaire[i].nom == "Electronique" && inventoryP2.inventaire[i].tier == 2){
				inv2[i] = iconElectronicT2;
			}else if(inventoryP2.inventaire[i].nom == "Electronique" && inventoryP2.inventaire[i].tier == 3){
				inv2[i] = iconElectronicT3;
			}else{
				inv2[i] = null;
			}
		}

		if(equipementP1.sword == 0){
			equipP1[0] = iconWepT0;
		}else if(equipementP1.sword == 1){
				equipP1[0] = iconWepT1;
		}else if(equipementP1.sword == 2){
			equipP1[0] = iconWepT2;
		}else if(equipementP1.sword == 3){
			equipP1[0] = iconWepT3;
		}

		if(equipementP1.armor == 0){
			equipP1[1] = iconArmorT0;
		}else if(equipementP1.armor == 1){
			equipP1[1] = iconArmorT1;
		}else if(equipementP1.armor == 2){
			equipP1[1] = iconArmorT2;
		}else if(equipementP1.armor == 3){
			equipP1[1] = iconArmorT3;
		}

		if(equipementP1.boots == 0){
			equipP1[2] = iconBootsT0;
		}else if(equipementP1.boots == 1){
			equipP1[2] = iconBootsT1;
		}else if(equipementP1.boots == 2){
			equipP1[2] = iconBootsT2;
		}else if(equipementP1.boots == 3){
			equipP1[2] = iconBootsT3;
		}

		if(equipementP1.gathering == 0){
			equipP1[3] = iconGatT0;
		}else if(equipementP1.gathering == 1){
			equipP1[3] = iconGatT1;
		}else if(equipementP1.gathering == 2){
			equipP1[3] = iconGatT2;
		}else if(equipementP1.gathering == 3){
			equipP1[3] = iconGatT3;
		}

		/////////
		/// Player 2
		/////////

		if(equipementP2.sword == 0){
			equipP2[0] = iconWepT0;
		}else if(equipementP2.sword == 1){
			equipP2[0] = iconWepT1;
		}else if(equipementP2.sword == 2){
			equipP2[0] = iconWepT2;
		}else if(equipementP2.sword == 3){
			equipP2[0] = iconWepT3;
		}

		if(equipementP2.armor == 0){
			equipP2[1] = iconArmorT0;
		}else if(equipementP2.armor == 1){
			equipP2[1] = iconArmorT1;
		}else if(equipementP2.armor == 2){
			equipP2[1] = iconArmorT2;
		}else if(equipementP2.armor == 3){
			equipP2[1] = iconArmorT3;
		}

		if(equipementP2.boots == 0){
			equipP2[2] = iconBootsT0;
		}else if(equipementP2.boots == 1){
			equipP2[2] = iconBootsT1;
		}else if(equipementP2.boots == 2){
			equipP2[2] = iconBootsT2;
		}else if(equipementP2.boots == 3){
			equipP2[2] = iconBootsT3;
		}

		if(equipementP2.gathering == 0){
			equipP2[3] = iconGatT0;
		}else if(equipementP2.gathering == 1){
			equipP2[3] = iconGatT1;
		}else if(equipementP2.gathering == 2){
			equipP2[3] = iconGatT2;
		}else if(equipementP2.gathering == 3){
			equipP2[3] = iconGatT3;
		}

		GUI.DrawTexture (new Rect (0 ,Screen.height - Screen.height*150/910,Screen.width / 2,Screen.height*150/910), blueBG);
		GUI.Box (new Rect (0 ,Screen.height - Screen.height*180/910,Screen.width*250/1617,Screen.height*30/910), "");
		////
		/// Healtbar
		GUI.Box (new Rect (Screen.width*5/1617, Screen.height - Screen.height*174/910, Screen.width*240/1617, Screen.height*20/910), "", currentStyle2);
		//vie laaaaaa
		if(pct1 > 0) GUI.Box (new Rect (Screen.width*6/1617, Screen.height - Screen.height*173/910, (int)(pct1*Screen.width*238/1617), Screen.height*18/910), "", currentStyle);
		/// 
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*70/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv[4]);
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*70/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv[9]);
		////
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*130/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv[3]);
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*130/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv[8]);
		////
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*190/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv[2]);
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*190/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv[7]);
		////
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*250/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv[1]);
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*250/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv[6]);
		////
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*310/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv[0]);
		GUI.Box (new Rect ((Screen.width / 2)-Screen.width*310/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv[5]);
		////
		/// 
		GUI.Box (new Rect (Screen.width*20/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP1[0]);
		GUI.Label (new Rect (Screen.width * 25 / 1617, Screen.height - Screen.height * 40 / 910, 60, 20), GetDurability (equipementP1.swordDurability));
		GUI.Box (new Rect (Screen.width*140/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP1[1]);
		GUI.Label (new Rect (Screen.width * 145 / 1617, Screen.height - Screen.height * 40 / 910, 60, 20), GetDurability (equipementP1.armorDurability));
		GUI.Box (new Rect (Screen.width*260/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP1[2]);
		GUI.Label (new Rect (Screen.width * 265 / 1617, Screen.height - Screen.height * 40 / 910, 60, 20), GetDurability (equipementP1.bootsDurability));
		GUI.Box (new Rect (Screen.width*380/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP1[3]);
		GUI.Label (new Rect (Screen.width * 385 / 1617, Screen.height - Screen.height * 40 / 910, 60, 20), GetDurability (equipementP1.gatheringDurability));
		////
		/// 
		/// 
		/// 
		/// 
		/// 
		GUI.DrawTexture(new Rect (Screen.width / 2 + 1,Screen.height - Screen.height*150/910,Screen.width / 2 -1,Screen.height*150/910), redBG);
		GUI.Box (new Rect (Screen.width / 2 + 1 ,Screen.height - Screen.height*180/910,Screen.width*250/1617,Screen.height*30/910), "");
		////
		/// /// Healtbar
		GUI.Box (new Rect (Screen.width / 2 + Screen.width*5/1617, Screen.height - Screen.height*174/910, Screen.width*240/1617, Screen.height*20/910), "", currentStyle2);
		if(pct2 > 0) GUI.Box (new Rect (Screen.width / 2 + Screen.width*6/1617, Screen.height - Screen.height*173/910, (int)(pct2*Screen.width*238/1617), Screen.height*18/910), "", currentStyle);
		////
		GUI.Box (new Rect ((Screen.width)-Screen.width*70/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv2[5]);
		GUI.Box (new Rect ((Screen.width)-Screen.width*70/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv2[9]);
		////
		GUI.Box (new Rect ((Screen.width)-Screen.width*130/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv2[3]);
		GUI.Box (new Rect ((Screen.width)-Screen.width*130/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv2[8]);
		////
		GUI.Box (new Rect ((Screen.width)-Screen.width*190/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv2[2]);
		GUI.Box (new Rect ((Screen.width)-Screen.width*190/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv2[7]);
		////
		GUI.Box (new Rect ((Screen.width)-Screen.width*250/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv2[1]);
		GUI.Box (new Rect ((Screen.width)-Screen.width*250/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv2[6]);
		////
		GUI.Box (new Rect ((Screen.width)-Screen.width*310/1617 ,Screen.height - Screen.height*130/910,Screen.width*50/1617,Screen.height*50/910), inv2[0]);
		GUI.Box (new Rect ((Screen.width )-Screen.width*310/1617 ,Screen.height - Screen.height*70/910,Screen.width*50/1617,Screen.height*50/910), inv2[5]);
		////
		/// 
		GUI.Box (new Rect (Screen.width / 2 + Screen.width*20/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP2[0]);
		GUI.Label (new Rect (Screen.width / 2 + Screen.width*25/1617 ,Screen.height - Screen.height*40/910,60,20), GetDurability(equipementP2.swordDurability));
		GUI.Box (new Rect (Screen.width / 2 + Screen.width*140/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP2[1]);
		GUI.Label (new Rect (Screen.width / 2 + Screen.width*145/1617 ,Screen.height - Screen.height*40/910,60,20), GetDurability(equipementP2.armorDurability));
		GUI.Box (new Rect (Screen.width / 2 + Screen.width*260/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP2[2]);
		GUI.Label (new Rect (Screen.width / 2 + Screen.width*265/1617 ,Screen.height - Screen.height*40/910,60,20), GetDurability(equipementP2.bootsDurability));
		GUI.Box (new Rect (Screen.width / 2 + Screen.width*380/1617 ,Screen.height - Screen.height*130/910,Screen.width*110/1617,Screen.height*110/910), equipP2[3]);
		GUI.Label (new Rect (Screen.width / 2 + Screen.width*385/1617 ,Screen.height - Screen.height*40/910,60,20), GetDurability(equipementP2.gatheringDurability));
	
	
		//Ligne du millieu
		Texture2D lineTex= new Texture2D(1, 1);
		Color tempColor = GUI.color;
		GUI.color = Color.black;
		GUI.DrawTexture(new Rect(Screen.width / 2-1, 0, 3, Screen.height), lineTex);
		GUI.color = tempColor;
	
	}
}