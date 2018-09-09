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
