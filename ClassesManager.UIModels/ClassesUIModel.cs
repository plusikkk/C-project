using ClassesManager.CommonComponents.Enums;
using ClassesManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.UIModels
{
    public class ClassesUIModel
    {
        private ClassesDBModel _dbModel;
        private Guid _subjectId;
        private DateOnly _date;
        private TimeOnly _startTime;
        private TimeOnly _endTime;
        private string _themeOfClass;
        private TypeOfClass _typeOfClass;
        private int _classDuration;

        public Guid? Id 
        { 
            get => _dbModel?.Id; 
        }
        public Guid SubjectId 
        { 
            get => _subjectId; 
        }
        public DateOnly Date 
        { 
            get => _date; 
            set => _date = value; 
        }
        public TimeOnly StartTime 
        { 
            get => _startTime; 
            set => _startTime = value; 
        }
        public TimeOnly EndTime 
        { 
            get => _endTime; 
            set => _endTime = value; 
        }
        public string ThemeOfClass 
        { 
            get => _themeOfClass; 
            set => _themeOfClass = value; 
        }
        public TypeOfClass TypeOfClass 
        { 
            get => _typeOfClass; 
            set => _typeOfClass = value; 
        }
        public int ClassDuration 
        { 
            get => _classDuration;
            set => CalculateClassDuration();
        }

        public ClassesUIModel(Guid subjectId)
        {
            _subjectId = subjectId;
        }

        public ClassesUIModel(ClassesUIModel dbModel)
        {
            _dbModel = dbModel;
            _subjectId = dbModel.SubjectId;
            _date = dbModel.Date;
            _startTime = dbModel.StartTime;
            _endTime = dbModel.EndTime;
            _themeOfClass = dbModel.ThemeOfClass;
            _typeOfClass = dbModel.TypeOfClass;
            CalculateClassDuration();
        }

        private void CalculateClassDuration()
        {
            if (_endTime < _startTime)
            {
                throw new InvalidOperationException();
            } 
            else
            {
                _classDuration = (int)(_endTime - _startTime).TotalMinutes;
            }
        }
    }
}
