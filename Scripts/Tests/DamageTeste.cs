using Godot;
using System;

public partial class DamageTeste : Area2D
{
    private void _on_body_entered(Player body)
    {
        if (body is Player Player)
        {
            Player.TakeDamage(1);
        }
    }

}
