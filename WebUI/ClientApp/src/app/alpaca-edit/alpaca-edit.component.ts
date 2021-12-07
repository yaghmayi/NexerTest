import { Component } from '@angular/core';
import { AlpacaService } from "../services/alpaca.service";
import { IAlpaca } from "../interfaces/alpaca.interface";
import {Router} from '@angular/router';
import { IFarm } from "../interfaces/farm.interface";

@Component({
  selector: 'alpaca-edit',
  templateUrl: './alpaca-edit.component.html',
})

export class AlpacaEditComponent {
  alpaca: IAlpaca = {};
  farms : IFarm[] = [];

  oldColorValue : string = null;

  pageTitle: string;
  saveButtonCaption : string;
  deleteButtonVisibility: boolean;

  constructor(private readonly service: AlpacaService, private readonly route:Router) {
  }

  ngOnInit() {
    this.loadFarms();
    let id = this.getIdParam();


    if (id === "-1") {
        this.pageTitle = "New Alpaca";
        this.saveButtonCaption = "Save";
        this.deleteButtonVisibility = false;

        this.alpaca = {
           id: -1,
           farm: {}
        };

    } else {
        this.pageTitle = "Edit Alpaca";
        this.saveButtonCaption = "Update";
        this.deleteButtonVisibility = true;

        this.loadAlpaca(Number(id));
    }
  }

  onSave() {
    this.saveAlpaca();
  }

  onCancel() {
    this.route.navigate(['/listalpaca']);
  }

  onDelete() {
    if (window.confirm("Are you sure to delete the Alpaca?")) {
       this.service.deleteAlpaca(this.alpaca.id).subscribe(() => { this.route.navigate(['/listalpaca']); });
    }
  }

  private saveAlpaca() {
    this.service.saveAlpaca(this.alpaca).subscribe(() => { this.route.navigate(['/listalpaca']); });
  }

  private loadAlpaca(id: number) {
    this.service.getAlpaca(id).subscribe(result => {
        this.alpaca = result;
        this.oldColorValue = this.alpaca.color;
      }
    );
  }

  private loadFarms() {
    this.service.getFarmList().subscribe(result =>
      this.farms = result
    );
  }

  getIdParam() : string {
      const parameters = new URLSearchParams(window.location.search);
      let idValue = parameters.get("id");

      return idValue;
  }

  isValidForm() : boolean {
    let isValid = true;

    if (this.isNullOrEmpty(this.alpaca.name) || this.isNullOrEmpty(this.alpaca.weight) || this.isNullOrEmpty(this.alpaca.farm.id)) {
      isValid = false;
    }

    return isValid;
  }

  isNullOrEmpty(val: number | string) : boolean {
    return val == null || val === '' || val.toString().trim() === '';
  }

  preventBlueColor() {
    const hexColor = this.alpaca.color;

    const r = parseInt(hexColor.substr(1, 2), 16);
    const g = parseInt(hexColor.substr(3, 2), 16);
    const b = parseInt(hexColor.substr(5, 2), 16);

    if (b > r && b > g) {
      this.alpaca.color = this.oldColorValue;
    } else {
      this.oldColorValue = this.alpaca.color;
    }
  }
}
