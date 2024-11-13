# SEPB-IGCG

## Installation

1. Copy the contents of Program.cs into an editor of your choosing.
2. Delete all code outside the lines commented "START PROGRAM; CONFIG START" and "END PROGRAM".
3. Change any configuration values you desire.
4. Move the modified script into your Programmable Block in-game.

## Configuration

All configuration values will be listed here, and explained for easy use.

- ISTRACKER
  - This value denotes whether the block running the program is providing or receiving guidance information.
  - Default is True. At least one grid must have this set to False for any visible effect.
- CAMERA_NAME
  - This value is the name of the camera that will be used to provide guidance information for the rest of the script.
  - Default is "CAM_TGP".
  - A camera with this name *must* exist on the grid as the Programmable Block if ISTRACKER is True.
  - While this can be any non-null, non-empty value, a unique name is recommended to ensure the correct camera is called.
- ANTENNA_NAME
  -  This value is the name of the antenna that will be used to either transmit or receive guidance information.
  -  Default is "Antenna".
  -  An antenna with this name *must* exist on the same grid as the Programmable Block.
  - This can be any non-null, non-empty value, but it should be noted that the antenna's range will not be modified, so this value should be set carefully.
- TARGET_RANGE
  - This value is the maximum range, in metres, the script will attempt to raycast for tracking purposes.
  - Default is 5000.
  - Any positive, non-null, non-zero value is acceptable, but larger ranges may take longer to aqcuire a lock.
- BROADCAST_TAG
  - This value is an identifier used to separate guidance systems.
  - Default is "IGCG".
  - Changing this value is *highly* recommended to prevent conflict with other guidance systems.
  - Application behaviour is undefined if multiple broadcasters use the same tag. 
