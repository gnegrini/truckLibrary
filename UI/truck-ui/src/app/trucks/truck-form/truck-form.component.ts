import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TruckService } from '../truck.service';
import { Truck } from '../truck.model';
import { MessageDialogComponent } from '../../shared/message-dialog/message-dialog.component';
import { MatDialog } from '@angular/material/dialog';



@Component({
  selector: 'app-truck-form',
  templateUrl: './truck-form.component.html',
  styleUrls: ['./truck-form.component.scss']
})
export class TruckFormComponent implements OnInit {
  form: FormGroup;
  modelOptions: string[] = ['FH', 'FM', 'VM'];
  plantOptions: string[] = ['Brazil', 'Sweden', 'United States', 'France'];


  constructor(
    private fb: FormBuilder,
    private truckService: TruckService,
    private dialogRef: MatDialogRef<TruckFormComponent>,
    private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: Truck | null
  ) {
    this.form = this.fb.group({
      model: ['', Validators.required],
      manufacturingYear: [2025, Validators.required],
      chassisCode: [
        this.data?.chassisCode || '',
        [
          Validators.required,
          Validators.pattern(/^[a-zA-Z0-9]{9}$/) // 9 caracteres alfanuméricos
        ]
      ],
      color: ['', Validators.required],
      manufacturingPlant: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    if (this.data) {
      this.form.patchValue(this.data);
    }
    console.log("created")
  }

  save() {
    if (this.form.valid) {
      
      let request$ = null;

      if (this.data) {
        const truckData = { id: this.data.id, ...this.form.value };
        request$ = this.truckService.update(this.data.id, truckData);
      } else {
        const truckData = this.form.value;
        request$ = this.truckService.create(truckData);
      }
      request$.subscribe({
        next: () => {
          this.dialogRef.close(true); // fecha o form
          this.dialog.open(MessageDialogComponent, {
            width: '300px',
            data: {
              title: 'Success',
              message: this.data ? 'Truck updated successfully!' : 'Truck created successfully!',
              isError: false
            }
          });
        },
        error: (err) => {
          const apiMessage = err.error?.error || 'Something went wrong. Please try again.';

          this.dialog.open(MessageDialogComponent, {
            width: '300px',
            data: {
              title: 'Error',
              message: apiMessage,
              isError: true
            }
          });
        }
      });

    }
  }

  cancel() {
    this.dialogRef.close();
  }
}
