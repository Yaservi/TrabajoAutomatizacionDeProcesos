import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MaestroService } from '../../../services/maestros/maestro-service';

// Custom validators
export function noNumbersValidator(): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const hasNumbers = /\d/.test(value);
    return hasNumbers ? { containsNumbers: true } : null;
  };
}

export function noRepeatedLettersValidator(maxRepetitions: number = 4): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const repeatedLettersPattern = new RegExp(`(.)\\1{${maxRepetitions},}`, 'i');
    const hasRepeatedLetters = repeatedLettersPattern.test(value);
    return hasRepeatedLetters ? { repeatedLetters: { maxRepetitions } } : null;
  };
}

export function onlyNumbersValidator(): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    // Remove the prefix if it exists (we'll handle it separately in the UI)
    const phoneNumber = value.toString().replace(/^\+\d+\s/, '');
    const hasNonNumbers = /[^\d]/.test(phoneNumber);
    return hasNonNumbers ? { containsNonNumbers: true } : null;
  };
}

export function dateMinimumYearsAgoValidator(years: number = 5): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const selectedDate = new Date(value);
    const today = new Date();
    const minimumDate = new Date();
    minimumDate.setFullYear(today.getFullYear() - years);

    return selectedDate > minimumDate ? { dateNotOldEnough: { requiredYears: years } } : null;
  };
}

@Component({
  selector: 'app-maestro-register',
  standalone: true,
  templateUrl: './maestro-register.component.html',
  styleUrl: './maestro-register.component.css',
  imports: [ReactiveFormsModule, FormsModule, CommonModule]
})
export class MaestroRegisterComponent implements OnChanges {
  @Input() maestro: any = null; // Recibe el maestro a editar (o null para nuevo)
  @Output() submitted = new EventEmitter<void>();
  maestroForm: FormGroup;
  mensaje: string = '';
  isEdit: boolean = false;
  prefijos: string[] = ['+1', '+52', '+34', '+57', '+58', '+51', '+56', '+54', '+55', '+593'];
  prefijoSeleccionado: string = '+52'; // Default to Mexico

  constructor(private fb: FormBuilder, private maestroService: MaestroService) {
    this.maestroForm = this.fb.group({
      id: [null], // Para edición
      nombre: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        noNumbersValidator(),
        noRepeatedLettersValidator(4)
      ]],
      apellido: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        noNumbersValidator(),
        noRepeatedLettersValidator(4)
      ]],
      direccion: ['', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(100)
      ]],
      fechaNacimiento: ['', [
        Validators.required,
        dateMinimumYearsAgoValidator(5)
      ]],
      telefono: ['', [
        Validators.required,
        Validators.minLength(7),
        Validators.maxLength(15),
        onlyNumbersValidator()
      ]],
      email: ['', [
        Validators.required,
        Validators.email,
        Validators.maxLength(100)
      ]],
      materia: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50)
      ]]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['maestro']) {
      if (this.maestro) {
        this.isEdit = true;
        this.maestroForm.patchValue(this.maestro);
      } else {
        this.isEdit = false;
        this.maestroForm.reset();
      }
      this.mensaje = '';
    }
  }

  onSubmit() {
    if (this.maestroForm.valid) {
      if (this.isEdit && this.maestro?.id) {

        this.maestroService.actualizarMaestro(this.maestro.id, this.maestroForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Maestro actualizado exitosamente!';
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al actualizar el maestro.';
          }
        });
      } else {
        this.maestroService.registrarMaestro(this.maestroForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Maestro registrado exitosamente!';
            this.maestroForm.reset();
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al registrar el maestro.';
          }
        });
      }
    }
  }
}
