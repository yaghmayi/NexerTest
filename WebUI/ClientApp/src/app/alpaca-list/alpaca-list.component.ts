import { Component } from '@angular/core';
import { AlpacaService } from "../services/alpaca.service";
import { IAlpaca } from "../interfaces/alpaca.interface";
import { IGroupItem } from "../interfaces/groubItem";

@Component({
  selector: 'alpaca-list',
  templateUrl: './alpaca-list.component.html',
})

export class AlpacaListComponent {
  alpacaItems: IAlpaca[] = [];
  groupItems: IGroupItem[] = [];

  constructor(private readonly service: AlpacaService) {
  }

  ngOnInit() {
    this.loadAlpacaItems();
  }

  private loadAlpacaItems() {
    this.service.getAlpacaList().subscribe(result => { this.alpacaItems = result; });
  }

  isThereSelectedAlpaca(): boolean {
    const findIndex = this.alpacaItems.findIndex(x => x.isSelected);

    return findIndex !== -1;
  }

  refreshGroupItems() {
    this.groupItems = [];

    let totalCost = 0;

    this.alpacaItems.forEach(alpacaItem => {

      if (alpacaItem.isSelected) {
        totalCost += alpacaItem.cost;

        let groupItem : IGroupItem = this.groupItems.find(x => x.farmName === alpacaItem.farm.name);
        

        if (groupItem != null) {
          groupItem.alpacasCount++;
          groupItem.cost = groupItem.cost + alpacaItem.cost;
        } else {
          groupItem = {};
          groupItem.farmName = alpacaItem.farm.name;
          groupItem.alpacasCount = 1;
          groupItem.cost = alpacaItem.cost;

          this.groupItems.push(groupItem);
        }
      }
    });

    let totalRow: IGroupItem = {
       cost: totalCost,
       isTotalRow: true
    };

    this.groupItems.push(totalRow);
  }


}
