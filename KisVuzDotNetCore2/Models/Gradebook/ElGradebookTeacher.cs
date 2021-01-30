using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Преподаватель
    /// </summary>
    public class ElGradebookTeacher
    {
        /// <summary>
        /// УИД преподавателя
        /// </summary>
        public int ElGradebookTeacherId { get; set; }

        /// <summary>
        /// ФИО преподавателя
        /// </summary>
        public string TeacherFio { get; set; }

        /// <summary>
        /// УИД аккаунта преподавателя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// УИД журнала
        /// </summary>
        public int ElGradebookId { get; set; }
        /// <summary>
        /// Журнал
        /// </summary>
        public ElGradebook ElGradebook { get; set; }
    }

    // Custom comparer for the ElGradebookTeacher class
    class ElGradebookTeacherComparer : IEqualityComparer<ElGradebookTeacher>
    {
        // ElGradebookTeacher are equal if their IserIds and ElGradebookIds are equal.
        public bool Equals(ElGradebookTeacher x, ElGradebookTeacher y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the ElGradebookTeacher' properties are equal.
            return x.UserId == y.UserId /*&& x.ElGradebookId == y.ElGradebookId*/;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(ElGradebookTeacher elGradebookTeacher)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(elGradebookTeacher, null)) return 0;

            //Get hash code for the UserId field if it is not null.
            int hashUserId = elGradebookTeacher.UserId == null ? 0 : elGradebookTeacher.UserId.GetHashCode();

            //Get hash code for the ElGradebookId field.
            //int hashElGradebookId = elGradebookTeacher.ElGradebookId.GetHashCode();

            //Calculate the hash code for the product.
            return hashUserId /*^ hashElGradebookId*/;
        }
    }
}