using System;
using System.Collections.Generic;

public class EntryPoint : Singleton<EntryPoint>
{
    List<IDisposable> Disposables = new List<IDisposable>();

    private void Awake() {
        CreateServiceLocators();
        CreateManagers();
    }

    void CreateServiceLocators(){
        Disposables.AddRange(new List<IDisposable>{
            new PlayableDirectorsSL(),
            new TransformsSL(),
            new ManagersSL(),
            new GameObjectsSL(),
            new ComponentsSL()
        });
    }

    void CreateManagers(){
        new PhysicsManager();
        new TimeManager();
        new GameManager();
        new SoundManager();
        new ParticleManager();
    }

    private void OnDisable() {
        foreach (var disposable in Disposables)
        {
            disposable.Dispose();
        }
    }
}
