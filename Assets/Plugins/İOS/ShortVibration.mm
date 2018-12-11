#import <Foundation/Foundation.h>
#import <AudioToolbox/AudioToolbox.h>
#import "ShortVibration.h"
#import <UIKit/UIKit.h>

extern "C"
{
    void Vibrate(int x)
    {
        if(x == 1)
        {
            UInt32 pop = SystemSoundID(1520);
            AudioServicesPlaySystemSound(pop);
        }
        else if (x == 2)
        {
            //UInt32 pop = SystemSoundID(1102);
            AudioServicesPlaySystemSound(kSystemSoundID_Vibrate);
        }
    }
}

