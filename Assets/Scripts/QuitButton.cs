using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitButton : Button {

  new public ParticleSystem particleSystem;

  public override void OnPointerDown(PointerEventData eventData) {
    particleSystem.Play();
  }

  public override void OnPointerUp(PointerEventData eventData) {
    particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
  }
}
