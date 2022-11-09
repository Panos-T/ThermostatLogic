using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;

namespace ThermostatLogic
{
    public class Thermostat
    {
        //Events for SIMPL+
       
        public delegate void myDelegate(ushort keepOn, ushort keepOff);
        public myDelegate thEvent { get; set;}

        public bool thMode,tempInc;
        public ushort temp, setpoint, offsetInc, offsetDec, previousTemp;

       
        

        //Get SIMPL+ commands
        public void setMode(ushort mode)
        {
            thMode = (mode==1) ? true : false;
        }

        public void getOffset(ushort currentOffsetInc, ushort currentOffsetDec)
        {
            offsetInc = currentOffsetInc;
            offsetDec = currentOffsetDec;
        }

        public void getData(ushort currentTemp, ushort currentSetpoint)
        {
            previousTemp = temp;    //<----------Possible Exception Error
            temp = currentTemp;
            setpoint = currentSetpoint;
            if (previousTemp <= temp)
            {
                tempInc = true;
            }
            else
            {
                tempInc = false;
            }
        }


        //Algorithm implementation
        public void thermostatAlgorithm()
        {
        
            if (thMode)
            {
                if (tempInc)
                {
                    if (setpoint - temp > offsetInc)
                    {
                        turnOn();
                    }
                    else
                    {
                        turnOff();
                    }
                }
                else
                {
                    if (temp - setpoint > offsetInc)
                    {
                        turnOn();
                    }
                    else
                    {
                        turnOff();
                    }
                }
            }
            else
            {
                turnOff();
            }
        }



        public void turnOn()
        {
            thEvent(1, 0);
        }

        public void turnOff()
        {
            thEvent(1, 0);
        }
    }
}
