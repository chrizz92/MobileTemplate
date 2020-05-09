using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileLibrary
{
    /// <summary>
    /// Simulation of a simple mobile phone
    /// </summary>
    public class Mobile
    {
        /// <summary>
        /// Fields
        /// </summary>
        private string _phoneNumber;

        private string _name;

        //Gespraechspartner
        private Mobile _passivePartner;

        private Mobile _activePartner;

        //Gespraechsinformationen
        private string _lastCalledNumber;

        private int _secondsActive;
        private int _secondsPassive;
        private bool _hasActiveCall;
        private DateTime _startingTime;
        private int _centsToPay;

        /// <summary>
        /// Constructors
        /// </summary>

        public Mobile(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public Mobile(string phoneNumber, string name) : this(phoneNumber)
        {
            Name = name;
        }

        /// <summary>
        /// Properties
        /// </summary>

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            private set { }
        }

        public string LastCalledNumber
        {
            get
            {
                return _lastCalledNumber;
            }
            private set { }
        }

        public int SecondsActive
        {
            get
            {
                return _secondsActive;
            }
        }

        public int SecondsPassive
        {
            get
            {
                return _secondsPassive;
            }
        }

        //public bool HasActiveCall
        //{
        //    get
        //    {
        //        return _hasActiveCall;
        //    }
        //}

        public int CentsToPay
        {
            get
            {
                return _centsToPay;
            }
        }

        /// <summary>
        /// With Errorhandling (see taskdescription)
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null && value.Length >= 2 && !int.TryParse(value, out int result))
                {
                    _name = value;
                }
                else
                {
                    _name = "ERROR";
                }
            }
        }

        /// <summary>
        /// Mobile initiates a call to a passive mobile phone. Time starts counting
        /// for both mobiles.
        /// </summary>
        /// <param name="passive">passive mobile</param>
        /// <returns>Returns true when phone call started correctly. False when active or passive phone is already busy (already talking).</returns>
        public bool StartCallTo(Mobile passive)
        {
            if (!_hasActiveCall && !passive._hasActiveCall)
            {
                _passivePartner = passive;
                passive._activePartner = this;
                _startingTime = DateTime.Now;
                _hasActiveCall = true;
                _passivePartner._hasActiveCall = true;
                _lastCalledNumber = _passivePartner._phoneNumber;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// End the call and also the call by the other mobile. Calculate duration and
        /// by the active caller the costs of the call.
        /// </summary>
        /// <returns>false, if there is no call pending</returns>
        public bool StopCall()
        {
            if (_hasActiveCall)
            {
                if (_passivePartner != null)
                {
                    DateTime endTime = DateTime.Now;                                    //Zeit zum Gespraechsende
                    int duration = (int)((endTime - _startingTime).TotalSeconds * 20);  //Dauer des Gespraechs
                    _passivePartner._secondsPassive += duration;                        //Passive Minuten addieren
                    _secondsActive += duration;                                         //Aktive Minuten addieren
                    _passivePartner._hasActiveCall = false;                             //Gespraech beenden
                    _passivePartner.StopCall();                                         //Anruf beim Passiven beenden
                    _passivePartner = null;                                             //Gespraechspartner entfernen
                    _hasActiveCall = false;                                             //Anruf beim Aktiven beenden

                    _centsToPay = _secondsActive / 60 * 8;
                    if (_secondsActive % 60 < 30 && _secondsActive % 60 > 0)
                    {
                        _centsToPay += 4;
                    }
                    else if (_secondsActive % 60 > 30)
                    {
                        _centsToPay += 8;
                    }
                }
                else
                {
                    if (_hasActiveCall)
                    {
                        _activePartner.StopCall();
                        _activePartner = null;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}