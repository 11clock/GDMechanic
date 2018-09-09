# GDMechanic
An API designed to streamline C# with Godot Engine without changing any fundamentals. It utilizes cached reflection for optimal performance, and provides power to easily extend the API with your own attributes.

## Status
GDMechanic is functionally complete, but a few features are not fully tested.

Most documentation is provided, but extensions are not yet documented.

GDMechanic was created with Godot 3.1 alpha, but should theoretically work with older 3.X versions.

## Wiring
GDMechanic provides a set of core attributes for wiring fields, properties, and methods.

### Activation
Call `this.Wire()` in the node's _Ready() method.

```cs
public override void _Ready()
{
	this.Wire();
	...
}
```
It is recommended to create a template for wired nodes, like the below.
```cs
using Godot;
using GDMechanic.Wiring;
using GDMechanic.Wiring.Attributes;

public class %CLASS% : %BASE%
{
	
	
	
	public override void _Ready()
	{
		this.Wire();
	}
}
```
You can place this template in Godot's "script_templates" folder in order for the IDE to pick it up.

### Core Attributes

#### Node
Returns a reference to a node based on the provided node path.

```cs
[Node("UI/Score")] private RichTextLabel _scoreText;
```

#### Child
Returns a reference to a child based on match type.

```cs
[Child] private Player _player;
// or
[Child(Matchtypes.Name)] private Player _player;

[Child(MatchTypes.Type)] private RichTextLabel _score;
```

#### Sibling
Returns a reference to a sibling based on match type.

#### Parent
Returns a reference to the node's parent.

```cs
[Parent] private Mommy _mommy;
```

#### Group
Adds this node to the specified group.

```cs
[Group("Huggables")]
public class Teddy : Node2D
{
	...
}
```

#### SignalReceiver
Connects the specified signal from the specified source. The source can either be a node path or a reference from a field on the same node.

```cs
[Child] private Button _button2;

[SignalReceiver("Button", "pressed")]
public void OnButtonPressed()
{
	...
}

[SignalReceiver("_button2", "pressed", SourceTypes.Reference)]
public void OnButton2Pressed()
{
	...
}
```

#### Timer
Adds a child Timer node with the specified criteria, and assigns a reference to it to the field.

```cs
[Timer(nameof(OnTimerTimeout), waitTime: 1.5f, oneShot: true)]
private Timer _timer;
```

##### Starting Timer with Custom Wait Time
GDMechanic provides an extension method to Timer that makes it easy to start a timer with a different wait time, without overriding the default.
```cs
_timer.Start(6f);
```

## Rng
Reimplementation of GDScript's "rand" functions.
It uses System.Random under the hood, so it will not have the same output as GDScript's "rand" functions.

```cs
Rng.RandRange(0f, 6f);
```

### Rng.Chance
Randomly returns true or false, based on the specified ratio.

```cs
if (Rng.Chance(0.05f))
{
	GD.Print("Critical hit!");
}
```

## TimerSystem
Node that organizes a collection of timers for easy access through delegates. It is designed to be used with TimerSystemAttribute and TimerReceiverAttribute, although it is fully functional on its own.

```cs
[TimerSystem]
private TimerSystem _timerSystem;

public void StartTimer() {
	_timerSystem.Start(OnTimerTimeout);
}
[TimerReceiver(waitTime: 0.25f, oneShot: true)]
public void OnTimerTimeout()
{
	...
}

[TimerReceiver(waitTime: 3f, oneShot: true)]
public void OnDeathTimeout()
{
	...
}
```

Alternatively you can place TimerReceiverAttribute on the same field as the TimerSystem reference.

```cs
[TimerSystem]
[TimerReceiver(nameof(OnTimerTimeout), waitTime: 0.25f, oneShot: true)]
private TimerSystem _timerSystem;

public void StartTimer() {
	_timerSystem.Start(OnTimerTimeout);
}

public void OnTimerTimeout()
{
	...
}

[TimerReceiver(waitTime: 3f, oneShot: true)]
public void OnDeathTimeout()
{
	...
}
```
