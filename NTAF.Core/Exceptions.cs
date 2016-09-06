using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NTAF.Core {
    #region Execptions
    public class PermissionException : System.Exception {
        public PermissionException() { }
        public PermissionException(string message) : base(message) { }
        public PermissionException(string message, Exception innerException) : base(message, innerException) { }
        }

    public class ClipperException : System.Exception {
        public ClipperException() { }
        public ClipperException(string message) : base(message) { }
        public ClipperException(string message, Exception innerException) : base(message, innerException) { }
        }

    public class NullPasswordException : System.Exception {
        public NullPasswordException() { }
        public NullPasswordException(string message) : base(message) { }
        public NullPasswordException(string message, Exception innerException) : base(message, innerException) { }
        }

    /// <summary>
    /// Thrown when the file is locked and cannot be edited
    /// </summary>
    public class FileLockedException : System.Exception {
        /// <summary>
        /// Initializes a new instance of the FileLockedException class.
        /// </summary>
        public FileLockedException() { }
        /// <summary>
        /// Initializes a new instance of the FileLockedException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FileLockedException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the FileLockedException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerEcecption">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public FileLockedException(string message, Exception innerEcecption) : base(message, innerEcecption) { }
        }

    public class ReadOnlyException : System.Exception {
        public ReadOnlyException() { }
        public ReadOnlyException(string message) : base(message) { }
        public ReadOnlyException(string message, Exception innerEcecption) : base(message, innerEcecption) { }
        }

    public class InvalidPasswordException : System.Exception {
        public InvalidPasswordException() { }
        public InvalidPasswordException(string message) : base(message) { }
        public InvalidPasswordException(string message, Exception innerException) : base(message, innerException) { }
        }

    public class RaceException : System.Exception {
        public RaceException() { }
        public RaceException(string message) : base(message) { }
        public RaceException(string message, Exception innerEcecption) : base(message, innerEcecption) { }
        }

    public class ItemException : System.Exception {
        public ItemException() { }
        public ItemException(string message) : base(message) { }
        public ItemException(string message, Exception innerEcecption) : base(message, innerEcecption) { }
        }

    public class UserCanceledException : System.Exception {
        public UserCanceledException() { }
        public UserCanceledException(string message) : base(message) { }
        public UserCanceledException(string message, Exception innerEcecption) : base(message, innerEcecption) { }
        }

    public class ValidationException : System.Exception {
        public readonly List<FieldAndMessage>
            Errors;

        public override string Message {
            get {
                string
                    retval = base.Message + "\n";

                foreach (FieldAndMessage err in Errors) {
                    retval += err.Field + ":" + err.Message + "\n";
                    }

                return retval;
                }
            }

        public ValidationException() : base("A validation error has occurd with no explicit reason given") { }

        public ValidationException(FieldAndMessage[] FieldAndError)
            : base("A validation error has occured in the following fields") {
            Errors = new List<FieldAndMessage>(FieldAndError); ;
            }

        public ValidationException(string message, FieldAndMessage[] FieldAndError, Exception innerEcecption) : base(message, innerEcecption) {
            Errors = new List<FieldAndMessage>(FieldAndError); ;
            }
        }

    public class ValidationWarning : System.Exception {
        public readonly List<FieldAndMessage>
            Errors;

        public override string Message {
            get {
                string
                    retval = base.Message + "\n";

                foreach (FieldAndMessage err in Errors) {
                    retval += err.Field + ":" + err.Message + "\n";
                    }

                retval += "Do you wish to continue with this action?";

                return retval;
                }
            }

        public ValidationWarning() : base("A validation error has occurd with no explicit reason given") { }

        public ValidationWarning(FieldAndMessage[] FieldAndWarning)
            : base("A validation warning has occured in the following fields") {
            Errors = new List<FieldAndMessage>(FieldAndWarning); ;
            }

        public ValidationWarning(string message, FieldAndMessage[] FieldAndWarning, Exception innerEcecption)
            : base(message, innerEcecption) {
            Errors = new List<FieldAndMessage>(FieldAndWarning); ;
            }
        }

    public class FieldAndMessage {
        public readonly string
            Field,
            Message;

        public FieldAndMessage(String field, String message) {
            Field = field;
            Message = message;
            }

        }

    public class NTFileExecption : System.Exception {
        private string fileName;
        public string FileName { get { return fileName; } }
        public NTFileExecption(string Filename) : base(string.Format("File {0} could not be saved", Filename)) { fileName = Filename; }
        public NTFileExecption(string Filename, string message) : base(message) { fileName = Filename; }
        public NTFileExecption(string Filename, string message, Exception innerException) : base(message, innerException) { fileName = Filename; }
        }

    public class InvalidParameter : System.Exception {
        public InvalidParameter() { }
        public InvalidParameter(string message) : base(message) { }
        public InvalidParameter(string message, Exception innerException) : base(message, innerException) { }
        }

    public class IDNotSupportedExecption : Exception {
        public IDNotSupportedExecption() { }
        public IDNotSupportedExecption(string message) : base(message) { }
        public IDNotSupportedExecption(string message, Exception innerException) : base(message, innerException) { }
        }

    public class NameNotSupportedExecption : Exception {
        public NameNotSupportedExecption() { }
        public NameNotSupportedExecption(string message) : base(message) { }
        public NameNotSupportedExecption(string message, Exception innerException) : base(message, innerException) { }
        }
    #endregion
    }
