import { Component, inject } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filters-dialog',
  imports: [
    MatDivider,
    MatSelectionList, //Dùng để hiển thị danh sách các lựa chọn lọc.
    MatListOption, //Dùng để hiển thị danh sách các lựa chọn lọc.
    MatButton, // UI Material giúp làm đẹp giao diện.
    FormsModule
  ],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss'
})
export class FiltersDialogComponent {
  shopService = inject(ShopService);
  private dialogRef = inject(MatDialogRef<FiltersDialogComponent>); //Điều khiển đóng mở hộp thoại.
  data = inject(MAT_DIALOG_DATA); //Nhận dữ liệu từ ShopComponent khi mở hộp thoại.

  selectedBrands: string[] = this.data.selectedBrands;
  selectedTypes: string[] = this.data.selectedTypes;

  applyFilters() {
    this.dialogRef.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedTypes,
    })
  }
}
