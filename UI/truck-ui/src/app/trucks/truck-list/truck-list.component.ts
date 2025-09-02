import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Truck } from '../truck.model';
import { TruckService } from '../truck.service';
import { TruckFormComponent } from '../truck-form/truck-form.component';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-truck-list',
  templateUrl: './truck-list.component.html',
  styleUrls: ['./truck-list.component.scss']
})
export class TruckListComponent implements OnInit {
  displayedColumns: string[] = ['model', 'manufacturingYear', 'chassisCode', 'color', 'manufacturingPlant', 'actions'];
  dataSource = new MatTableDataSource<Truck>();
  filterValue = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private truckService: TruckService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadTrucks();
  }

  loadTrucks() {
    this.truckService.getAll().subscribe(data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.filterValue = filterValue.trim().toLowerCase();
    this.dataSource.filter = this.filterValue;
  }

  clearFilter() {
    this.filterValue = '';
    this.dataSource.filter = '';
  }

  addTruck() {
    console.log("Add truck")
    const dialogRef = this.dialog.open(TruckFormComponent, { width: '400px', data: null });
    dialogRef.afterClosed().subscribe(() => this.loadTrucks());
  }

  editTruck(truck: Truck) {
    const dialogRef = this.dialog.open(TruckFormComponent, { width: '400px', data: truck });
    dialogRef.afterClosed().subscribe(() => this.loadTrucks());
  }

  deleteTruck(id: string) {
    // if (confirm('Are you sure you want to delete this truck?')) {
    //   this.truckService.delete(id).subscribe(() => this.loadTrucks());
    // }

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: {
        title: 'Confirm Delete',
        message: 'Are you sure you want to delete this truck?'
      }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.truckService.delete(id).subscribe(() => this.loadTrucks());
      }
    });

  }
}
