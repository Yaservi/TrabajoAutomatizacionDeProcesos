
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CalificacionService, Calificacion } from '../../../services/calificacion/calificacion.service';
import { AlumnoService, Alumno } from '../../../services/alumnos/alumno-service';
import { MateriaService, Materia } from '../../../services/materias/materia-service';

@Component({
  standalone: true,
  selector: 'app-update-calificacion',
  templateUrl: './update_calificacion.component.html',
  styleUrls: ['./update_calificacion.component.scss'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class UpdateCalificacionComponent implements OnChanges, OnInit {
  @Input() calificacion!: Calificacion;
  @Output() volver = new EventEmitter<void>();
  //@Output() actualizado = new EventEmitter<void>();  // <-- Nuevo output para notificar actualización

  @Output() actualizado = new EventEmitter<Calificacion>();


  calificacionForm: FormGroup;
  alumnos: Alumno[] = [];
  materias: Materia[] = [];

  constructor(
    private fb: FormBuilder,
    private calificacionService: CalificacionService,
    private alumnoService: AlumnoService,
    private materiaService: MateriaService
  ) {
    this.calificacionForm = this.fb.group({
      idAlumno: ['', Validators.required],
      idMateria: ['', Validators.required],
      participacion: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      primerParcial: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      segundoParcial: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      examenFinal: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      trabajoInvestigacion: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      trabajoFinal: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      nota: [{ value: 0, disabled: true }, Validators.required]
    });

    this.calificacionForm.valueChanges.subscribe(() => this.calcularNotaFinal());
  }

  ngOnInit(): void {
    this.cargarAlumnos();
    this.cargarMaterias();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['calificacion'] && this.calificacion) {
      this.calificacionForm.patchValue({
        idAlumno: this.calificacion.idAlumno,
        idMateria: this.calificacion.idMateria,
        participacion: this.calificacion.participacion,
        primerParcial: this.calificacion.primerParcial,
        segundoParcial: this.calificacion.segundoParcial,
        examenFinal: this.calificacion.examenFinal,
        trabajoInvestigacion: this.calificacion.trabajoInvestigacion,
        trabajoFinal: this.calificacion.trabajoFinal,
        nota: this.calificacion.nota
      });
    }
  }

  cargarAlumnos() {
    this.alumnoService.getTodosAlumnos().subscribe({
      next: alumnos => this.alumnos = alumnos,
      error: err => {
        console.error('Error cargando alumnos', err);
        this.alumnos = [];
      }
    });
  }

  cargarMaterias() {
    this.materiaService.obtenerTodasMaterias().subscribe({
      next: materias => this.materias = materias,
      error: err => {
        console.error('Error cargando materias', err);
        this.materias = [];
      }
    });
  }

  calcularNotaFinal() {
    const form = this.calificacionForm;

    const participacion = Number(form.get('participacion')?.value) || 0;
    const primerParcial = Number(form.get('primerParcial')?.value) || 0;
    const segundoParcial = Number(form.get('segundoParcial')?.value) || 0;
    const examenFinal = Number(form.get('examenFinal')?.value) || 0;
    const trabajoInvestigacion = Number(form.get('trabajoInvestigacion')?.value) || 0;
    const trabajoFinal = Number(form.get('trabajoFinal')?.value) || 0;

    const notaFinal =
      participacion * 0.10 +
      primerParcial * 0.20 +
      segundoParcial * 0.20 +
      examenFinal * 0.25 +
      trabajoInvestigacion * 0.10 +
      trabajoFinal * 0.15;

    form.get('nota')?.setValue(notaFinal.toFixed(2), { emitEvent: false });
  }

  //actualizarCalificacion() {
  //  if (this.calificacionForm.valid) {
  //    const datos = this.calificacionForm.getRawValue(); // incluye 'nota' aunque esté disabled

  //    const payload = {
  //      ...datos,
  //      id: this.calificacion.id
  //    };

  //    this.calificacionService.actualizarCalificacion(payload).subscribe({
  //      next: () => {
  //        alert('Calificación actualizada correctamente');
  //        this.actualizado.emit();  // <-- Emitimos el evento para que el padre refresque
  //        this.volver.emit();
  //      },
  //      error: (err) => {
  //        console.error('Error al actualizar calificación', err);
  //        alert('Error al actualizar la calificación');
  //      }
  //    });
  //  }
  //}

  //actualizarCalificacion() {
  //  if (this.calificacionForm.valid) {
  //    const datos = this.calificacionForm.getRawValue(); // incluye 'nota' aunque esté disabled

  //    const payload = {
  //      ...datos,
  //      id: this.calificacion.id
  //    };

  //    this.calificacionService.actualizarCalificacion(payload).subscribe({
  //      next: (calificacionActualizada) => {
  //        alert('Calificación actualizada correctamente');
  //        this.actualizado.emit(calificacionActualizada);  // Enviamos el objeto actualizado al padre
  //        this.volver.emit();
  //      },
  //      error: (err) => {
  //        console.error('Error al actualizar calificación', err);
  //        alert('Error al actualizar la calificación');
  //      }
  //    });
  //  }
  //}

  actualizarCalificacion() {
    if (this.calificacionForm.valid) {
      const datos = this.calificacionForm.getRawValue(); // incluye 'nota' aunque esté disabled

      const payload = {
        ...datos,
        id: this.calificacion.id
      };

      this.calificacionService.actualizarCalificacion(payload).subscribe({
        next: (calificacionActualizada: Calificacion) => {
          alert('Calificación actualizada correctamente');
          this.actualizado.emit(calificacionActualizada); // ✅ Emitimos el objeto actualizado al padre
          this.volver.emit(); // ✅ Cerramos el modal
        },
        error: (err) => {
          console.error('Error al actualizar calificación', err);
          alert('Error al actualizar la calificación');
        }
      });
    }
  }


  cancelar() {
    this.volver.emit();
  }
}

