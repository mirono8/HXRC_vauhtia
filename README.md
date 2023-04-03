# Speechly:n käyttö

Yhdellä käyttäjätilillä voi olla monta eri Speechly-instanssia koulutettu kuuntelemaan tiettyjä sanoja ja esimerkkilauseita

Jokaisella instanssilla on oma app-id, jota käytetään integroinnissa

Applikaatiolla on kolme ydin-osaa:

 1. Intent
 2. Entity
 3. Muuttujat

## Intent

Intent on annetun esimerkkilauseen kuvaus kiteytetty yhteen sanaan

![image](/uploads/14402061060a4c8d4063167606222093/image.png)

Intent määritetään applikaatiossa (vas.) ja kouluttaessa lauseet merkitään tähdillä "*" (oik.) ja intent-sanalla = *paina

Tällöin kaikki määritetyt variaatiot tästä lauseesta luokitellaan “paina”- avainsanan alle

## Entity

Tiettyjen sanojen merkkaaminen yhden avainsanan alle

![image](/uploads/4325671308ab0076a82a839cd3b46a1e/image.png)

Entity:t määritellään applikaatiossa (vas.), ja koulutuslauseissa ne merkataan suluilla "(kahvinkeitin)" (oik.)

Käyttäjä voi merkata minkä tahansa sanan haluamallaan entity:llä, jos se auttaa tarkentamaan lausetta

Entity:n avulla erotellaan samankaltaiset lauseet, jottei niitä tunnisteta vääränä intent:nä

## Muuttujat

Esimerkkilauseiden kirjoittamisessa voi suoraviivaistaa koulutusta kirjoittamalla lauseita, sanoja ja listoja muuttujiksi

![image](/uploads/c9b3029b05aef89a24dc0275f98a2b2f/image.png)

Muuttujat alustetaan ennen esimerkkilauseita ja merkataan syntaksissa $-merkillä

Listoissa (merkintätapa [ x | y | z] ) applikaatio valitsee yhden sanan listassa koulutuksen aikana,
toisin sanoen harjoittaa saman lauseen kolmella eri muuttujalla, ilman että jokaista variaatiota tarvitsisi kirjoittaa uudestaan


## Syntaksi

Esimerkkilause: ![image](/uploads/b0b1cd0a9e6cfe8dd16bababc343b3b7/image.png)

Lause koostuu osista:

	- *paina = intent

	- [&laita_lause] = lista-muuttuja

	- $kahvinkeitin(kahvinkeitin) = lista-muuttuja, joka on merkattu entity:llä (kahvinkeitin)

	- {päälle} = valinnainen sana, eli ei aina esiinny kouluttaessa tätä lausetta.
	             Toisin sanoen lause on tunnistettavissa tämän sanan kanssa ja ilman sitä

Tämä yksi esimerkkilause vastaa näitä lauseita:

	laita kahvinkeitin
	laita kahvinkeitin päälle
	laita keitin
	laita keitin päälle
	laittakaa kahvinkeitin
	laittakaa kahvinkeitin päälle
	laittakaa keitin 
	laittakaa keitin päälle…

ja 12 muuta variaatiota lauseista

jokainen lause tulkitaan intent:n “paina” alle


![image](/uploads/eed23301e1ec9e495660911516568b7d/image.png)

Esimerkkilauseissa voi myös määrittää permutaatioita merkkaamalla listan merkillä “!”

Näin applikaatio kouluttaa lauseen niin, että merkityn listan muuttujien paikkaa voi vaihtaa

“Kaada kahvi kahvikuppiin” tai “Kaada kahvikuppiin kahvi”

### Speechly:n oma dokumentaatio

Speechly:n oma dokumentaatio kertoo syventävämmin: 

https://dreamy-cori-a02de1.netlify.app/slu-examples/basics/

