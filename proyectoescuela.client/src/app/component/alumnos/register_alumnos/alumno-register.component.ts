import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AlumnoService } from '../../../services/alumnos/alumno-service';

@Component({
  selector: 'app-alumno-register',
  standalone: true,
  templateUrl: './alumno-register.component.html',
  styleUrls: ['./alumno-register.component.css'],
  imports: [ReactiveFormsModule, CommonModule]
})
export class AlumnoRegisterComponent implements OnChanges {
  @Input() alumno: any = null; // Recibe el alumno a editar (o null para nuevo)
  @Output() submitted = new EventEmitter<void>();
  alumnoForm: FormGroup;
  mensaje: string = '';
  isEdit: boolean = false;

  constructor(private fb: FormBuilder, private alumnoService: AlumnoService) {
    this.alumnoForm = this.fb.group({
      id: [null], // Para edición
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      direccion: ['', Validators.required],
      fechaNacimiento: ['', Validators.required],
      telefono: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['alumno']) {
      if (this.alumno) {
        this.isEdit = true;
        this.alumnoForm.patchValue(this.alumno);
      } else {
        this.isEdit = false;
        this.alumnoForm.reset();
      }
      this.mensaje = '';
    }
  }

  onSubmit() {
    if (this.alumnoForm.valid) {
      if (this.isEdit && this.alumno?.id) {
        // Editar
        this.alumnoService.actualizarAlumno(this.alumno.id, this.alumnoForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Alumno actualizado exitosamente!';
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al actualizar el alumno.';
          }
        });
      } else {
        this.alumnoService.registrarAlumno(this.alumnoForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Alumno registrado exitosamente!';
            this.alumnoForm.reset();
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al registrar el alumno.';
          }
        });
      }
    }
  }
}
