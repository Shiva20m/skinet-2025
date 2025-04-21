import { Component, inject } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filters-dialog',
  standalone:true,
  imports: [MatDivider, MatSelectionList, MatListOption, MatButton, FormsModule],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss'
})
export class FiltersDialogComponent {
  shopService = inject(ShopService);
  // we are passing the data via the shopcomponent to the FiltersDialogComponent by dialog
  private dialogRef = inject(MatDialogRef<FiltersDialogComponent>);
  data = inject(MAT_DIALOG_DATA);
  // now this data has the access what user selected
  selectedBrands: string[] = this.data.selectedBrands;
  selectedTypes: string[]= this.data.selectedTypes;
  constructor() {
    if (this.selectedBrands && this.selectedTypes) {
      console.log('Selected Brands before the selection:', this.selectedBrands);
      console.log('Selected Types before the selection:', this.selectedTypes);
    }
  }
  // now apply the filter
  applyFilters()
  {
    this.dialogRef.close({
      selectedBrands:this.selectedBrands,
      selectedTypes:this.selectedTypes
    })
  }

}
