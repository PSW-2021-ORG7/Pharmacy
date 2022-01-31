import { Component} from '@angular/core';
import { RegisterService } from './register.service';
import { IMedicine } from '../model/medicine';
import Swal from 'sweetalert2'
import 'sweetalert2/dist/sweetalert2.all.min.js'



@Component({
  selector: 'app-register-medicine',
  templateUrl: './register-medicine.component.html',
  styleUrls: ['./register-medicine.component.css'],
  providers: [RegisterService]
})
export class RegisterMedicineComponent {
  constructor(private _registerService: RegisterService) { }
  name: string = '';
  dosageInMg: any= '';
  sideEffects: string[] = [];
  manufacturer: string = '';
  sideEffectInput: string = '';
  reactions: string[] = [];
  reactionInput: string = '';
  potentialDanger: string = '';
  potentialDangerInput: string = '';
  description: string = '';
  wayOfConsumption: string = '';
  nameError: boolean = false;
  dosageError: boolean = false;
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

  onSubmit(): void{
    if(this.name === '')this.nameError=true
    else this.nameError=false;
    if(this.dosageInMg == '' || isNaN(this.dosageInMg))this.dosageError=true;
    else this.dosageError=false;
    if(this.dosageError || this.nameError)
      return
    const Medicine: IMedicine = {
      'Name' : this.name,
       'Description': this.description,
       'DosageInMilligrams': parseInt(this.dosageInMg),
       'Manufacturer': this.manufacturer,
       'SideEffects' : this.sideEffects,
       'PossibleReactions': this.reactions,
       'WayOfConsumption' : this.wayOfConsumption,
       'PotentialDangers' : this.potentialDanger,
    }
    this._registerService.saveMedicine(Medicine)
      .subscribe(data => console.log(data));
    
    console.log(JSON.stringify(Medicine))

    Swal.fire(
      'Success!',
      'Medicine successfully registered!',
      'success'
    )
  }

}
