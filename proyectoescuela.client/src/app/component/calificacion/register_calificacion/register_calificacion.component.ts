

import { Component, OnInit, Output, EventEmitter } from '@angular/core'; // <- Importación añadida
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AlumnoService, Alumno } from '../../../services/alumnos/alumno-service';
import { MateriaService, Materia } from '../../../services/materias/materia-service';
import { CalificacionService } from '../../../services/calificacion/calificacion.service';

@Component({
  standalone: true,
  selector: 'app-register-calificacion',
  templateUrl: './register_calificacion.component.html',
  styleUrls: ['./register_calificacion.component.scss'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class RegisterCalificacionComponent implements OnInit {
  calificacionForm: FormGroup;
  alumnos: Alumno[] = [];
  materias: Materia[] = [];

  @Output() registrada = new EventEmitter<void>(); // <- Evento para informar al padre

  constructor(
    private fb: FormBuilder,
    private alumnoService: AlumnoService,
    private materiaService: MateriaService,
    private calificacionService: CalificacionService
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

    // Recalcular nota final al modificar cualquier campo
    this.calificacionForm.valueChanges.subscribe(() => {
      this.calcularNotaFinal();
    });
  }

  ngOnInit(): void {
    this.cargarAlumnos();
    this.cargarMaterias();
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

  registrarCalificacion() {
    if (this.calificacionForm.valid) {
      const datos = this.calificacionForm.getRawValue(); // incluye 'nota'

      this.calificacionService.registrarCalificacion(datos).subscribe({
        next: () => {
          alert('Calificación registrada correctamente');
          this.calificacionForm.reset();
          this.calificacionForm.get('nota')?.setValue(0);
          this.registrada.emit(); // <- Notificar al componente padre
        },
        error: (err) => {
          console.error('Error registrando calificación', err);
          alert('Error al registrar la calificación');
        }
      });
    }
  }
}
