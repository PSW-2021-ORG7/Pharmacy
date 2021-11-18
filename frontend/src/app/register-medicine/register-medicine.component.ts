import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register-medicine',
  templateUrl: './register-medicine.component.html',
  styleUrls: ['./register-medicine.component.css']
})
export class RegisterMedicineComponent implements OnInit {
  sideEffects: string[] = [];
  sideEffectInput: string = '';
  reactions: string[] = ['ee','ff','cc'];
  reactionInput: string = '';
  selectedFile: any = null;
  url: any='';
  constructor() { }

  addReaction(){
    if(this.reactionInput != ''){
      this.reactions.push(this.reactionInput)
      this.reactionInput= ''
    }
  }
  removeReaction(i: any){
    this.reactions.splice(i, 1);
  }

  addSideEffect(){
    console.log(this.sideEffects)
    if(this.sideEffectInput != ''){
      this.sideEffects.push(this.sideEffectInput)
      this.sideEffectInput= ''
    }
  }
  removeSideEffect(i: any){
    this.sideEffects.splice(i, 1);
  }

  onFileSelected(event: any){
    const files = event.target.files;
    if (files.length === 0)
        return;

    const mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
        return;
    }

    const reader = new FileReader();
    this.selectedFile = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
        this.url = reader.result; 
    }
  }

  ngOnInit(): void {
  }

}
