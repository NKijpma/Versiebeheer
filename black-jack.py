from spel_onderdelen import maak_deck, trek_kaart, bereken_score, print_hand,info
import os 
import random
import time
import emoji
#geimport voor de zekerheid

#blackjack

def clear_terminal():# chatgpt (docent zij dat ik het moest vragen aan chatgpt),(idee clear term van thijmen) alle clears zelf geplaatst
     os.system('cls' if os.name == 'nt' else 'clear') 

def speel_blackjack():
    clear_terminal()
    deck = maak_deck() #variable deck wordt functie maak_deck
    speler = [trek_kaart(deck) for _ in range(2)] #trekt 2 kaarten 
    dealer = [trek_kaart(deck) for _ in range(2)] #trekt 2 kaarten
    while True:
        print_hand(speler, "Speler") # print de hand van de spelere en text speler
        print_hand([dealer[0], ("?", "?")], "Dealer", toon_score=False) #  print kaarten dealer enn houdt een 1 ?
        keuze = input("\n(H)it or (S)tand? ").lower() # input met lowercase
        if keuze == "h": # als keuze h is
            clear_terminal()
            speler.append(trek_kaart(deck)) # stopt de nieuwe getrokken kaart in spelers hand
            if bereken_score(speler) > 21: # als speler score groter dan  21 print speler hand
                print_hand(speler, "Speler")
                print("\nBUST over de 21! Dealer wint.") 
                return
        elif keuze == "s":# als keuze stand is 
            clear_terminal()
            break #break if-loop
        else:
            clear_terminal()
            print("Ongeldige keuze!")# als keuze niet een van deze opties is print ongeldige keuze
        
    # dealer speelt
    while bereken_score(dealer) < 17: # als berekend dealer score onder 17 is
        dealer.append(trek_kaart(deck)) # voeg kaart toe aan dealer hand
        
    print_hand(speler, "Speler (eind)")  # print hand speler met speler eind erachter
    print_hand(dealer, "Dealer (eind)") # print hand dealer met dealer eind erachter

    s_score = bereken_score(speler)  # variable voor speler score
    d_score = bereken_score(dealer)  # variable voor dealer score

    if s_score > 21: # als speler score groter is dan 21
        print("\nDealer wint!\U0001F613") # print dealer wint     #\U0001F613 = 😓
    elif s_score == 21: # als speler score gelijk is aan 21 
        print("\nSpeler wint! \U0001F3C6") #print speler wint
    elif d_score > 21 or s_score > d_score: #als dealer score boven 21 of minder is dan speler
        print("\nSpeler wint! \U0001F3C6") # print speler wint
    elif s_score == d_score: # als speler score en dealer score gelijk is aan elkaar
        print("\nGelijkspel!\U0001F610")# print gelijkspel   # \U0001F610 =  😐
    else:
        print("\nDealer wint!\U0001F613")# als het niet 1 van deze bovenste gevallen is print dealer wint    #\U0001F613 = 😓

def main_menu(): # functie voor main menu
    while True:
        print("\n\U0001F0CF BLACKJACK \U0001F0CF")  # nieuwe regel blackjack   # \U0001F0CF = 🃏
        print("1.\u25B6\uFE0F ‎  Start spel") # print 1.start spel   # \u25B6\uFE0F =▶️   # ‎  is onzichtbaar char
        print("2.\u2139\uFE0F ‎  Info") # print 2.info   # \u2139\uFE0F = ℹ️
        print("3.\u274C  ‎Quit") # print 3.quit   # \u274C = ❌
        keuze = input("Maak een keuze: ") # print input maak een keuze:...
        
        clear_terminal()
        
        if keuze == "1": # als keuze 1 is
            speel_blackjack() # start blackjack opnieuw
            
        elif keuze == "2":# als keuze 2 is
            info()#print info
            
        elif keuze == "3": # als keuze 3 is
            print("Tot ziens \U0001F44B \U0001F44B") # print tot ziens   # \U0001F44B = 👋
            break # en breekt de loop
        
        else:
            print("Ongeldige keuze!")# als keuze niet een van deze opties is print ongeldige keuze
            
            
main_menu() # roept functie main menu om game te starten (ik weet dat je zo stack overflow kan krijgen maar ik ben lui 😵‍💫)
