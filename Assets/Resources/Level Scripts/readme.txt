A level's XML document is fairly simple, and is defined by the following tags:

<Level>:
Whatever's contained inside is what's loaded into the script. Can have the following attributes:

"level"
The ID of this level.

Inside there can only be <Objects>:
  <Object>:
  Represents an object to be loaded. Can have the following attributes:

  "offset"
  How much further to the right should the object be spawned.

  Can have the following inner tags:
    <Prefab>:
    The name of the Unity prefab to use for this object.
    <Distance>(optional):
    How far along the level this object should be spawned. Defaults to 0.
    <Height>(optional):
    At what height the object should be spawned. Defaults to 0.
    <Speed>(optional):
    The speed the object should be spawned with. Defaults to 5.
    <VSpeed>(optional):
    The speed the object should move vertically. Defaults to 0.
    <VThreshold>(optional):
    How far from the player the object should activate its vertical movement. Defaults to 16.
    <Aperture>(Gate prefab only):
    How wide should the gap between both pipes be when all the switches are activated.

    There are some special cases where an object may have inner tags representing more objects. These are:
      <Link>:
      Parsed when inside a Switch object. Same tags as a normal object. Intended so that the object calls Activate() when the switch is triggered.
      <Switch>:
      Parsed when inside a Gate object. Same tags as a normal object. Intended so that the gate opens a bit when the switch is triggered.