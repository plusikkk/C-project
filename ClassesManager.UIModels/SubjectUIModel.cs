using ClassesManager.CommonComponents.Enums;
using ClassesManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.UIModels
{
    public class SubjectUIModel
    {
        private SubjectDBModel _dbModel;
        private string _name;
        private float _ects;
        private FieldOfKnowledge _fieldOfKnowledge;
        private List<ClassesUIModel> _classes;
        private int _allClassesDuration;

        public Guid? Id
        {
            get => _dbModel?.Id;
        }
        public string Name 
        {
            get => _name; 
            set => _name = value; 
        }
        public float ECTS 
        {
            get => _ects;
            set => _ects = value;
        }
        public FieldOfKnowledge FieldOfKnowledge 
        {
            get => _fieldOfKnowledge;
            set => _fieldOfKnowledge = value; 
        }
        public IReadOnlyList<ClassesUIModel> Classes 
        { 
            get => _classes; 
        }
        public int AllClassesDuration 
        {
            get => _allClassesDuration;
            set => CalculateAllClassesDuration();
        }

        public SubjectUIModel()
        {
            _classes = new List<ClassesUIModel>();
        }

        public SubjectUIModel(SubjectDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _name = dbModel.Name;
            _ects = dbModel.ECTS;
            _fieldOfKnowledge = dbModel.FieldOfKnowledge;
            CalculateAllClassesDuration();
        }

        private void CalculateAllClassesDuration()
        {

        }
    }
}
