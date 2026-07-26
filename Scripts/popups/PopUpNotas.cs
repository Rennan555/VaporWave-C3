using Godot;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

/*Algoritmo para gerenciar o bloco de notas:
os textos,
e a janela pop up*/
public partial class PopUpNotas : Area2D
{
	//onready --> variaveis que se referem a nós presentes na arvore
	private RichTextLabel text;
	private Control popUp;

	private Timer time;

	private AnimationPlayer animationText;

	private Label textInteraact;
	//variaveis 
	private bool canClose; // será usado para determinar quando fechar a janela e desativar o bloco de notas
	private int linhaTexto;

	private bool canInteract = true;

	private bool InArea = false;
	private PopWindows11 windows11;

	[Export] public bool show;

	[Export] public string notes = "lorem jad jdad jad jad";
	
	// Signal de emitir resposta
	[Signal]
	public delegate void CallChangeLevelEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//chamado as variaveis
		text = GetNode<RichTextLabel>("popUp/Panel/RichTextLabel");
		popUp = GetNode<Control>("popUp");
		time = GetNode<Timer>("Timer");
		animationText = GetNode<AnimationPlayer>("popUp/Panel/RichTextLabel/AnimationText");
		textInteraact = GetNode<Label>("ActionArea/Label");
		windows11 = GetNode<PopWindows11>("Pop_windows11");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//se a variavel show for verdadeira
		if (show && !canClose)
		{
			ShowPopUp();
			animationText.Play("write");
			
			if (text.VisibleRatio >= 0.09f)
			{
				GD.Print("");
				animationText.Play("full");
				canClose = true;
			}
		}

		if (InArea)
		{
			//implementar a funcao para interagir
			if (Input.IsActionJustPressed("Action")){
				show = true;
				canInteract = false;
				GD.Print("açãõ");
				textInteraact.Visible = false;
			}
		}
		

	}

	//fechar a janela
	public void  _on_close_pressed()
	{
		GD.Print("fechou");
		//QueueFree();
		if (canClose) {
		popUp.Visible = false;
		windows11.Visible = true;
		AudioManager.Instance.PlayMouseClick();
		

		//adicionar a tela de atualizacao

		}
	}

	//mostrar a janela do bloco de notas
	public void ShowPopUp()
	{
		text.Text = notes;
		popUp.Visible = true;
		AudioManager.Instance.PlayPopUp();
		
	}

	public void _on_action_area_body_entered(Node2D body)
	{
		if (canInteract)
		{
			textInteraact.Visible = true;
			InArea = true;
		}
		

		
	}

	public void _on_action_area_body_exited(Node2D body)
	{
		textInteraact.Visible = false;
		InArea = false;
	}

	public void _on_pop_windows_11_resposta(bool response)
	{
		if (response)
		{
			GD.Print();
			EmitSignal(SignalName.CallChangeLevel);
		}
	}
}
