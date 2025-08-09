import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AsistenciaService } from '../../../services/asistencia/asistencia-service';

@Component({
  selector: 'app-asistencia-register',
  standalone: true,
  templateUrl: './asistencia-register.component.html',
  styleUrls: ['./asistencia-register.component.css'],
  imports: [ReactiveFormsModule, CommonModule]
})
export class AsistenciaRegisterComponent implements OnChanges {
  @Input() asistencia: any = null;
  @Input() alumnos: any[] = [];
  @Input() materias: any[] = [];
  @Output() submitted = new EventEmitter<void>();

  asistenciaForm: FormGroup;
  mensaje: string = '';
  isEdit: boolean = false;

  constructor(private fb: FormBuilder, private asistenciaService: AsistenciaService) {
    this.asistenciaForm = this.fb.group({
      id: [null], // Para edición
      estado: ['', Validators.required],
      fechaAsistencia: ['', Validators.required],
      alumnoId: ['', Validators.required],
      materiaId: ['', Validators.required]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['asistencia']) {
      if (this.asistencia) {
        this.isEdit = true;

        // Format the date for the form
        const formattedAsistencia = { ...this.asistencia };

        if (formattedAsistencia.fechaAsistencia) {
          // Convert to YYYY-MM-DD format for the date input
          const date = new Date(formattedAsistencia.fechaAsistencia);
          formattedAsistencia.fechaAsistencia = date.toISOString().split('T')[0];
        }

        this.asistenciaForm.patchValue(formattedAsistencia);
      } else {
        this.isEdit = false;
        this.asistenciaForm.reset();
      }
      this.mensaje = '';
    }
  }

  onSubmit() {
    if (this.asistenciaForm.valid) {
      if (this.isEdit && this.asistencia?.id) {
        // Editar
        this.asistenciaService.actualizarAsistencia(this.asistencia.id, this.asistenciaForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Asistencia actualizada exitosamente!';
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al actualizar la asistencia.';
          }
        });
      } else {
        // Registrar
        this.asistenciaService.crearAsistencia(this.asistenciaForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Asistencia registrada exitosamente!';
            this.asistenciaForm.reset();
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al registrar la asistencia.';
          }
        });
      }
    }
  }
}
