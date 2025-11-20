import random

#spel onderdelen blackjack

#kaarten
type=["\u2660\uFE0F","\u2665\uFE0F","\u2666\uFE0F","\u2663\uFE0F",] # volgorde is :["♠️","♥️","♦️","♣️"]
waarden = {
    "A": 11,   # geeft elke cijfer/type kaart een waarde
    "K": 10,    
    "Q": 10,
    "J": 10,
    "10": 10,
    "9": 9,
    "8": 8,
    "7": 7,
    "6": 6,
    "5": 5,
    "4": 4,
    "3": 3,
    "2": 2}

#functies om te roepen
def maak_deck(): # maakt deck
    deck = [(waarde, soort) for waarde in waarden for soort in type] # plakt type en waarde samen voor deck
    random.shuffle(deck) # shuffle deck
    return deck

#kiest 1 kaart van deck en popt hem
def trek_kaart(deck):
 return deck.pop()

 #Berekent de totale waarde van een hand
def bereken_score(hand):
    score = sum(waarden[w] for w, s in hand) # summary waarden for kaart 1 en 2 in hand
    aantal_a = sum(1 for w, s in hand if w == "A") # als kaart ace is 
    while score > 21 and aantal_a > 0: # als score groter is dan 21  en aantak acen boven 1 
        score -= 10 # haal 10 af van score
        aantal_a -= 1 # en haal 1 aas weg
    return score

#print hand
def print_hand(hand, naam="Speler", toon_score=True):#print speler hand en score als toon_score waar is
    kaarten = "‎ ,‎ ".join([f"{w}{s}" for w, s in hand]) # print 2 kaarten 
    print(f"\n{naam}: {kaarten}")
    if toon_score:
        print(f"Score: {bereken_score(hand)}") # als toonscore waar is print score hand

#print spelregels
def info():
    print("\n===\U0001F0CF Blackjack Info \U0001F0CF===") # \U0001F0CF = 🃏
    print(" Kom zo dicht mogelijk bij 21.")
    print("② Aas telt als 1 of 11.\U0001F0A1") #\U0001F0A1 = 🂡
    print("③ Boer, Vrouw, Heer = 10 punten.")
    print("④ Dealer trekt tot 17 punten.")
    print("=======================")