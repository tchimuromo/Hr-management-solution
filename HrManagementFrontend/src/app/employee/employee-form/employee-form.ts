import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { EmployeeService } from '../employee';
import { Employee } from '../models/employee.model';

@Component({
  selector: 'app-employee-form',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './employee-form.html',
  styleUrl: './employee-form.css',
})
export class EmployeeForm implements OnInit {
  employeeForm: FormGroup;
  isEditMode = false;
  employeeId: number | null = null;
  loading = false;
  error = '';
  submitted = false;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {
    this.employeeForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email]],
      phone: [''],
      department: ['', [Validators.required, Validators.maxLength(50)]],
      position: ['', [Validators.required, Validators.maxLength(50)]],
      hireDate: ['', Validators.required],
      salary: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.employeeId = Number(id);
      this.loadEmployee(this.employeeId);
    }
  }

  loadEmployee(id: number): void {
    this.loading = true;
    this.employeeService.getEmployee(id).subscribe({
      next: (employee) => {
        this.employeeForm.patchValue({
          firstName: employee.firstName,
          lastName: employee.lastName,
          email: employee.email,
          phone: employee.phone,
          department: employee.department,
          position: employee.position,
          hireDate: this.formatDate(employee.hireDate),
          salary: employee.salary
        });
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Failed to load employee';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  formatDate(date: Date | string): string {
    const d = new Date(date);
    const month = ('0' + (d.getMonth() + 1)).slice(-2);
    const day = ('0' + d.getDate()).slice(-2);
    return `${d.getFullYear()}-${month}-${day}`;
  }

  get f() {
    return this.employeeForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.error = '';

    if (this.employeeForm.invalid) {
      return;
    }

    this.loading = true;
    const employee: Employee = {
      id: this.employeeId || 0,
      ...this.employeeForm.value
    };

    if (this.isEditMode && this.employeeId) {
      this.employeeService.updateEmployee(this.employeeId, employee).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/employees']);
        },
        error: (err) => {
          this.error = 'Failed to update employee';
          this.loading = false;
          console.error(err);
        }
      });
    } else {
      this.employeeService.createEmployee(employee).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/employees']);
        },
        error: (err) => {
          this.error = 'Failed to create employee';
          this.loading = false;
          console.error(err);
        }
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/employees']);
  }
}
