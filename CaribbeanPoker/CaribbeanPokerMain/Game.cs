﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanPokerMain
{
    class Game
    {
        private readonly Gambler _gambler;
        private readonly Dealer _dealer;
        private readonly Deck _deck;
        private readonly View _view;
        private int _jackpot;
        private const int JackpotAnte = 10;
        private const int JackpotDefault = 10000;
        public Game()
        {

            _gambler = new Gambler();
            _dealer = new Dealer();
            _deck = new Deck();
            _view = new View();
            _jackpot = JackpotDefault;
        }

        public void Run()
        {
            _view.Clear();
            while (!_gambler.IsBroke())
            {
                _view.PrintStatus(_gambler.Money, _jackpot);
                var ante = _gambler.TheController.GetAnte();
                _gambler.Money -= (int)ante;
                var isJackpot = _gambler.TheController.GetAnswer("Do  you want to participate in the jackpot?");
                if (isJackpot) 
                {
                    _jackpot += JackpotAnte;
                    _gambler.Money -= (int)JackpotAnte;
                }
                _gambler.Hand.Cards = _deck.DequeueHand();
                _gambler.Hand.FlipCards(_gambler.Hand.Cards.Length, sorted: true, true);
                _dealer.Hand.Cards = _deck.DequeueHand();
                _dealer.Hand.FlipCards(1, sorted: false, true); // Reveal the dealer's first card 
                _view.DisplayBoard(_dealer.Hand.Cards, _gambler.Hand.SortedCards, _deck.CardBack,
                    playerCombination: _gambler.Hand.GetHandCombination().ToString());
                if (_gambler.TheController.GetAnswer("Do  you raise ?"))
                {
                    _gambler.Money -= 2*(int)ante;
                    _dealer.Hand.FlipCards(_dealer.Hand.Cards.Length, sorted: true, true);
                    _view.DisplayBoard(_dealer.Hand.SortedCards, _gambler.Hand.SortedCards, _deck.CardBack,
                        _dealer.Hand.GetHandCombination().ToString(), _gambler.Hand.GetHandCombination().ToString());
                    if (_dealer.IsQualify())
                    {
                        if (_gambler.Hand > _dealer.Hand)
                        {
                            _view.PrintMsg("You win!");
                            _gambler.Money += 3*(int)ante; // return player money
                            _gambler.Money += (int)ante;  // won dealer anteValues
                            _gambler.Money += RankMoney(_gambler.Hand.GetHandCombination(), ante, isJackpot); // won bet bonus money
                        }
                        else if (_dealer.Hand == _gambler.Hand)
                        {
                            _view.PrintMsg("Push!");
                            _gambler.Money += 3*(int)ante;
                        }
                        else
                        {
                            _view.PrintMsg("You lose!");
                        }
                    }
                    else
                    {
                        _view.PrintMsg("Dealer folds!");
                        _gambler.Money += 4*(int)ante;
                    }
                }
                _dealer.Hand.FlipCards(_dealer.Hand.Cards.Length, sorted: false, false);
                _gambler.Hand.FlipCards(_dealer.Hand.Cards.Length, sorted: false, false);
                _deck.EnqueueHand(_gambler.Hand.Cards);
                _deck.EnqueueHand(_dealer.Hand.Cards);
                _deck.Shuffle();
            }
            _view.PrintMsg("You are broke. Goodbye!");
            _gambler.TheController.Quit();         
        }
        private int RankMoney(HandCombination rank, int ante, bool isJackpot)
        {
            int money = 2*(int)ante; // betted money
            switch (rank)
            {
                case HandCombination.nothing:
                    money *= 1;
                    break;
                case HandCombination.full:
                    money *= 7;
                    break;
                case HandCombination.quads:
                    money *= 20;
                    break;
                case HandCombination.straight_flush:
                    money *= 50;
                    break;
                case HandCombination.royal_flush:
                    money *= 100;
                    break;
                default:
                    money *= ((int)rank-1);
                    break;
            }
            if (isJackpot && (int)rank > 5)
            {
                switch (rank)
                {
                    case HandCombination.flush:
                        money += 5*JackpotAnte;
                        _jackpot -= 5*JackpotAnte;
                        break;
                    case HandCombination.full:
                        money += 10*JackpotAnte;
                        _jackpot -= 10*JackpotAnte;
                        break;
                    case HandCombination.quads:
                        money += 50*JackpotAnte;
                        _jackpot -= 50*JackpotAnte;
                        break;
                    case HandCombination.straight_flush:
                        money += (int)(0.1*_jackpot);
                        _jackpot -= (int)(0.1*_jackpot);
                        break;
                    case HandCombination.royal_flush:
                        money += _jackpot;
                        _jackpot = 0;
                        break;
                }
                if (_jackpot < 0.5*JackpotDefault) _jackpot = JackpotDefault;
            }
            return money;
        }
    }
}