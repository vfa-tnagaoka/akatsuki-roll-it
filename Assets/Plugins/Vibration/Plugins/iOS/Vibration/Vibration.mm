//
//  Vibration.mm
//  https://videogamecreation.fr
//
//  Created by Benoît Freslon on 23/03/2017.
//  Copyright © 2018 Benoît Freslon. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <AudioToolbox/AudioToolbox.h>
#import <UIKit/UIKit.h>

#import "Vibration.h"

#define USING_IPAD UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad

@interface Vibration ()

@end

@implementation Vibration



//////////////////////////////////////////

#pragma mark - Vibrate

+ (BOOL)    hasVibrator {
    return !(USING_IPAD);
}
+ (void)    vibrate {
    AudioServicesPlaySystemSoundWithCompletion(1352, NULL);
}
+ (void)    vibratePeek {
   // AudioServicesPlaySystemSoundWithCompletion(kSystemSoundID_Vibrate, NULL); // Actuate `Peek` feedback (weak boom)
    if (@available(iOS 13.0, *)) {
        UIImpactFeedbackGenerator *myGen = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleSoft)];
        [myGen impactOccurred];
        myGen = NULL;
    } else {
        // Fallback on earlier versions
    }
    
}
+ (void)    vibratePop {
    AudioServicesPlaySystemSoundWithCompletion(1520, NULL); // Actuate `Pop` feedback (strong boom)
}
+ (void)    vibrateNope {
    AudioServicesPlaySystemSoundWithCompletion(1521, NULL); // Actuate `Nope` feedback (series of three weak booms)
}

+ (void)    vibrateHeavy {
    UIImpactFeedbackGenerator *myGen = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleHeavy)];
    [myGen impactOccurred];
    myGen = NULL;
    
}
+ (void)    vibrateMedium {
    UIImpactFeedbackGenerator *myGen = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleMedium)];
    [myGen impactOccurred];
    myGen = NULL;
}
+ (void)    vibrateLight {
    UIImpactFeedbackGenerator *myGen = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleLight)];
    [myGen impactOccurred];
    myGen = NULL;
}
+ (void)    vibrateSoft {
if (@available(iOS 13.0, *)) {
    UIImpactFeedbackGenerator *myGen = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleSoft)];
    [myGen impactOccurred];
    myGen = NULL;
} else {
    // Fallback on earlier versions
}
}
+ (void)    vibrateRigid {
if (@available(iOS 13.0, *)) {
    UIImpactFeedbackGenerator *myGen = [[UIImpactFeedbackGenerator alloc] initWithStyle:(UIImpactFeedbackStyleRigid)];
    [myGen impactOccurred];
    myGen = NULL;
} else {
    // Fallback on earlier versions
}
}


@end
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#pragma mark - "C"

extern "C" {
    
    //////////////////////////////////////////
    // Vibrate
    
    bool    _HasVibrator () {
        return [Vibration hasVibrator];
    }
 
    void    _Vibrate () {
        [Vibration vibrate];
    }
    
    void    _VibratePeek () {
        [Vibration vibratePeek];
    }
    void    _VibratePop () {
        [Vibration vibratePop];
    }
    void    _VibrateNope () {
        [Vibration vibrateNope];
    }
    

void    _VibrateHeavy () {
    [Vibration vibrateHeavy];
}

void    _VibrateMedium () {
    [Vibration vibrateMedium];
}

void    _VibrateLight () {
    [Vibration vibrateLight];
}

void    _VibrateSoft () {
    [Vibration vibrateSoft];
}

void    _VibrateRigid () {
    [Vibration vibrateRigid];
}

}

