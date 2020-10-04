<Query Kind="Program" />

void Main()
{
	Dictionary<String, float> uj = new Dictionary<String, float>();
	Dictionary<String, float> eredeti = new Dictionary<String, float>();

	
	
	uj.Add("Elso", 11.0f);
	uj.Add("Masodik", 12.0f);
	uj.Add("Harmadik", 15.8787f); //Valtozott ertek
	uj.Add("Negyedig",14.0f);
	uj.Add("Otodik",15.0f);
	//uj.Add("hatodik",16.0f); //Torolt kulcs
	uj.Add("Hetedik",17.0f); // Uj kulcs
	
	
	eredeti.Add("Elso", 11.0f);
	eredeti.Add("Masodik", 12.0f);
	eredeti.Add("Harmadik", 13.0f);
	eredeti.Add("Negyedig",14.0f);
	eredeti.Add("Otodik",15.0f);
	eredeti.Add("hatodik",16.0f);
	
	var uj_e_eredeti = uj.Except(eredeti);
	var eredeti_e_uj = eredeti.Except(uj);
	
	var eredeti_list = eredeti.Keys;
	var uj_list = uj.Keys;
	
	
	
	
	Console.WriteLine("Ujban valtozo vagy uj parameterek");
	Console.WriteLine(uj_e_eredeti);
	
	Console.WriteLine("Torolt Elemek");
	Console.WriteLine(eredeti_list.Except(uj_list));
	
	Console.WriteLine("Hozzadott Elemek");
	Console.WriteLine(uj_list.Except(eredeti_list));
	
	
	
}

// Define other methods and classes here
