import { ValidatorFn, AbstractControl } from '@angular/forms';
import { PasswordComplexityCheckerService } from '../password-complexity-checker.service';
export class RegistrationFormValidator {

    static passwordComplexityChecker(checker: PasswordComplexityCheckerService): ValidatorFn {
        return (control: AbstractControl): {
            [key: string]: any;
        } | null => {
            const isPasswordWeak = !checker.check(control.value);
            return isPasswordWeak ? { 'weakPassword': { value: control.value } } : null;
        };
    }

    static MatchPassword(AC: AbstractControl) {
        let password = AC.get('password').value;
        let confirmPassword = AC.get('passwordConfirmation').value;
        if (password == confirmPassword) {
            return
        }
        let errors = AC.get('passwordConfirmation').errors;
        if (errors == null)
            errors = {};
        errors.matchPassword = true;
        AC.get('passwordConfirmation').setErrors(errors)
    }
}
